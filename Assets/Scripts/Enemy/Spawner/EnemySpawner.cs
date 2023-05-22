using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner: MonoBehaviour
{
    [SerializeField] private SpawnerConfiguration configuration;
    [SerializeField] private List<Transform> spawnPoints;

    private BaseZombie.Factory _zombieFactory;
    private BaseBossZombie.Factory _bossZombieFactory;
    private ProgressBar _progressBar;

    private int _enemiesToSpawn;
    private int _killedEnemies;
    private int _targetProgress;
    private bool _stopSpawn;
    private bool _bossIsSpawned;
    private bool _bossIsKilled;

    [Inject]
    private void Construct(
        BaseZombie.Factory zombieFactory,
        BaseBossZombie.Factory bossZombieFactory,
        ProgressBar progressBar)
    {
        _zombieFactory = zombieFactory;
        _bossZombieFactory = bossZombieFactory;
        _progressBar = progressBar;
    }

    private void Start()
    {
        _enemiesToSpawn = (int) configuration.zombieAmount;
        _targetProgress = GetTargetProgress();
    }

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
        GetComponent<Collider>().enabled = false;
        _progressBar.Hide();
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