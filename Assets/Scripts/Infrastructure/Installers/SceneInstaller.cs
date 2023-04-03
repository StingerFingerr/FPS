using System.Collections.Generic;
using Zenject;

public class SceneInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        BindWeaponSlots();
        BindWeaponHolder();

        BindIInventory();
    }

    private void BindIInventory()
    {
        Container.BindInterfacesAndSelfTo<GridInventory>().AsSingle();
        //Container.Bind<IInventory>().To<GridInventory>().AsSingle();
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