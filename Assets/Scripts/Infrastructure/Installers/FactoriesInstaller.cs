using Weapons;
using Zenject;

public class FactoriesInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        BindGameFactory();
        BindWeaponFactory();

    }

    private void BindGameFactory() => 
        Container.Bind<GameFactory>().AsSingle();

    private void BindWeaponFactory() => 
        Container.BindFactory<string, WeaponBase, WeaponBase.Factory>().FromFactory<WeaponFactory>();
}