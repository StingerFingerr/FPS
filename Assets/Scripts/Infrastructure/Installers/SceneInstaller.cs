using System.Collections.Generic;
using UI.Game;
using Zenject;

public class SceneInstaller: MonoInstaller
{
    public OnHoverMessageView onHoverMessageView;
    
    public override void InstallBindings()
    {
        BindWeaponSlots();
        BindWeaponHolder();
        BindOnHoverMessageView();
    }

    private void BindOnHoverMessageView()
    {
        Container.Bind<OnHoverMessageView>().FromComponentInNewPrefab(onHoverMessageView).AsSingle();
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