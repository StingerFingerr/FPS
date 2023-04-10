using System.Collections.Generic;
using Effects;
using Shooting;
using UnityEngine;
using Zenject;

public class SceneInstaller: MonoInstaller
{
    [SerializeField] private PrefabService prefabs;
    
    public override void InstallBindings()
    {
        BindWeaponSlots();
        BindWeaponHolder();

        BindIInventory();

        BindBulletsPool();
        
        BindBulletsImpactsPool();
        BindBloodyBulletsImpactsPool();
    }

    private void BindBloodyBulletsImpactsPool()
    {
        Container.BindFactory<Vector3, Vector3, BloodyBulletImpact, BloodyBulletImpact.Factory>()
            .FromPoolableMemoryPool<Vector3, Vector3, BloodyBulletImpact, BloodyBulletImpact.Pool>(poolBinder => poolBinder
                .WithInitialSize(30)
                .FromComponentInNewPrefab(prefabs.bloodyBulletImpactPrefab)
                .UnderTransformGroup("Bullets impacts"));
    }

    private void BindBulletsImpactsPool()
    {
        Container.BindFactory<Vector3, Vector3, BulletImpact, BulletImpact.Factory>()
            .FromPoolableMemoryPool<Vector3, Vector3, BulletImpact, BulletImpact.Pool>(poolBinder => poolBinder
                .WithInitialSize(30)
                .FromComponentInNewPrefab(prefabs.bulletImpactPrefab)
                .UnderTransformGroup("Bullets impacts"));
    }

    private void BindBulletsPool()
    {
        Container.BindFactory<Vector3, Vector3, int, Bullet, Bullet.Factory>()
            .FromPoolableMemoryPool<Vector3, Vector3, int, Bullet, Bullet.Pool>(poolBinder => poolBinder
                .WithInitialSize(30)
                .FromComponentInNewPrefab(prefabs.bulletPrefab)
                .UnderTransformGroup("Bullets"));
    }

    private void BindIInventory() => 
        Container.BindInterfacesAndSelfTo<GridInventory>().AsSingle();


    private void BindWeaponHolder() => 
        Container.BindInterfacesAndSelfTo<WeaponHolder>().FromMethod(GetWeaponHolder);

    private void BindWeaponSlots() => 
        Container.BindInterfacesTo<WeaponSlot>().FromMethodMultiple(GetWeaponSlots);

    private WeaponHolder GetWeaponHolder(InjectContext arg) => 
        FindObjectOfType<WeaponHolder>();

    private IEnumerable<WeaponSlot> GetWeaponSlots(InjectContext arg) => 
        FindObjectsOfType<WeaponSlot>();
}