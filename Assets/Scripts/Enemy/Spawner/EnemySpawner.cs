using System.Collections.Generic;
using Game_logic;
using Infrastructure;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UniqueId))]
public class EnemySpawner: MonoBehaviour, IProgressReader, IProgressWriter, IProgressInitializer
{
    [SerializeField] private SpawnerConfiguration configuration;
    [SerializeField] private List<Transform> spawnPoints;

    private BaseZombie.Factory _zombieFactory;
    private BaseBossZombie.Factory _bossZombieFactory;
    private ProgressBar _progressBar;
    private IProgressService _progressService;
    private DiContainer _diContainer;
    private UniqueId _uniqueId;

    private Collider _collider;
    
    private int _enemiesToSpawn;
    private int _killedEnemies;
    private int _targetProgress;
    private bool _stopSpawn;
    private bool _bossIsSpawned;
    private bool _bossIsKilled;
    private bool _cleared;

    [Inject]
    private void Construct(
        BaseZombie.Factory zombieFactory,
        BaseBossZombie.Factory bossZombieFactory,
        ProgressBar progressBar,
        IProgressService progressService,
        DiContainer diContainer)
    {
        _zombieFactory = zombieFactory;
        _bossZombieFactory = bossZombieFactory;
        _progressBar = progressBar;
        _uniqueId = GetComponent<UniqueId>();
        _collider = GetComponent<Collider>();
        _progressService = progressService;
        _diContainer = diContainer;
    }

    public void Load(Progress progress)
    {
        if (progress.EnemySpawnerInfos.TryGetValue(_uniqueId.id, out EnemySpawnerInfo info))
        {
            _enemiesToSpawn = info.enemiesToSpawn;
            _killedEnemies = info.killedEnemies;
            _bossIsSpawned = info.bossIsKilled;
            _bossIsKilled = info.bossIsKilled;
            _stopSpawn = info.stopSpawn;
            if (info.cleared is false)
                _collider.enabled = true;
        }
        
        CalculateTargetProgress();
    }

    public void Save(Progress progress)
    {
        EnemySpawnerInfo info = new()
        {
            enemiesToSpawn = (int)configuration.zombieAmount - _killedEnemies,
            killedEnemies = _killedEnemies,
            bossIsKilled = _bossIsKilled,
            stopSpawn = _stopSpawn,
            cleared = _cleared
        };
        if (progress.EnemySpawnerInfos.ContainsKey(_uniqueId.id))
            progress.EnemySpawnerInfos[_uniqueId.id] = info;
        else
            progress.EnemySpawnerInfos.Add(_uniqueId.id, info);
    }

    public void InitializeProgressData()
    {
        _enemiesToSpawn = (int) configuration.zombieAmount;
        _collider.enabled = true;
        CalculateTargetProgress();
    }

    private void CalculateTargetProgress() => 
        _targetProgress = GetTargetProgress();

    private void Spawn()
    {
        var amountToSpawn = (int) configuration.maxAmountAtTime;
        amountToSpawn = Mathf.Clamp(amountToSpawn, 0, _enemiesToSpawn);
        
        if(NeedSpawnBoss())
            SpawnBoss();
        
        if(amountToSpawn == 0)
            return;
        
        for (int i = 0; i < amountToSpawn; i++)        
            SpawnZombie();

        UpdateProgress();
    }

    private void EnemyKilled()
    {
        _killedEnemies++;
        if(NeedSpawnBoss())
            SpawnBoss();
        if(_stopSpawn is false)
            SpawnZombie();
        UpdateProgress();
    }

    private void ReduceEnemiesToSpawn()
    {
        _enemiesToSpawn--;
        if (_enemiesToSpawn <= 0)
            _stopSpawn = true;
    }

    private bool NeedSpawnBoss()
    {
        if (configuration.hasBoos is false)
            return false;
        if (_bossIsSpawned)
            return false;

        return _killedEnemies >= configuration.spawnBossAfterKills;
    }

    private void BossKilled()
    {
        _bossIsKilled = true;
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        var progress = GetProgress();
        if (progress >= 1)
            MarkAsCleared();
        _progressBar.SetNewValue(progress);
    }

    private void MarkAsCleared()
    {
        _collider.enabled = false;
        _cleared = true;
        _progressBar.Hide();
        
        _progressService.InformProgressWritersForSave(_diContainer);
        _progressService.Save();
        Debug.Log("Enemy spawner cleared, progress saved");
    }

    private float GetProgress() => 
        (GetIntermediateProgress() * 1f / _targetProgress);

    private int GetIntermediateProgress()
    {
        return _killedEnemies + (
            configuration.hasBoos ? 
                _bossIsKilled ? 
                    configuration.bossWeight :
                    0 :
                0);
    }

    private int GetTargetProgress() => 
        (int)configuration.zombieAmount + (configuration.hasBoos ? configuration.bossWeight : 0);

    private void SpawnBoss()
    {
        SpawnFromFactory(_bossZombieFactory,GetRandomPoint() , BossKilled);
        _bossIsSpawned = true;
    }

    private void SpawnZombie()
    {
        if(_stopSpawn is false)
        {
            ReduceEnemiesToSpawn();
            SpawnFromFactory(_zombieFactory,GetRandomPoint(), EnemyKilled);
        }
    }

    private void SpawnFromFactory<TPos, TParameter, TType>(PlaceholderFactory<TPos, TParameter,TType> factory, TPos pos, TParameter onKilled) => 
        factory.Create(pos, onKilled);

    private Vector3 GetRandomPoint() => 
        spawnPoints[Random.Range(0, spawnPoints.Count)].position;

    private void OnTriggerEnter(Collider other)
    {
        _progressBar.Show();
        Spawn();
    }

    private void OnTriggerExit(Collider other) => 
        _progressBar.Hide();
}