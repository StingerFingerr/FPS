using Weapon;
using Zenject;

public class FactoriesInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        BindWeaponFactory();
    }

    private void BindWeaponFactory() => 
        Container.BindFactory<string, WeaponBase, WeaponBase.Factory>().FromFactory<WeaponFactory>();
}