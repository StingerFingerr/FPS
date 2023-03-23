using System.Collections.Generic;
using Game_runner;
using Zenject;

public class SceneInstaller: MonoInstaller
{
    public LevelInitializer levelInitializer;
        
    public override void InstallBindings()
    {
        BindLevelInitializer();
        
        BindWeaponSlots();
        BindWeaponHolder();
    }

    private void BindLevelInitializer()
    {
        Container.BindInstance(levelInitializer);

    }

    private void BindWeaponHolder() => 
        Container.BindInterfacesAndSelfTo<WeaponHolder>().FromMethod(GetWeaponHolder);

    private void BindWeaponSlots() => 
        Container.BindInterfacesTo<WeaponSlot>().FromMethodMultiple(GetWeaponSlots);

    private WeaponHolder GetWeaponHolder(InjectContext arg) => 
        FindObjectOfType<WeaponHolder>();

    private IEnumerable<WeaponSlot> GetWeaponSlots(InjectContext arg) => 
        FindObjectsOfType<WeaponSlot>();
}