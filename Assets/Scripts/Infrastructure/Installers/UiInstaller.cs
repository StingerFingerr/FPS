using Zenject;

public class UiInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        BindCachedCrosshairFactory();
    }

    private void BindCachedCrosshairFactory() =>
        Container.BindFactory<CrosshairType, DynamicCrosshairBase, DynamicCrosshairBase.CachedFactory>()
            .FromFactory<CrosshairCachedFactory>().NonLazy();
}