using Zenject;

public class UiInstaller: MonoInstaller
{
    public PrefabService prefabs;
    
    public override void InstallBindings()
    {
        BindCachedCrosshairFactory();
        BindProgressBar();
    }

    private void BindProgressBar() => 
        Container.Bind<ProgressBar>().FromComponentInNewPrefab(prefabs.progressBarPrefab).AsSingle();

    private void BindCachedCrosshairFactory() =>
        Container.BindFactory<CrosshairType, DynamicCrosshairBase, DynamicCrosshairBase.CachedFactory>()
            .FromFactory<CrosshairCachedFactory>();
}