using Infrastructure;
using Zenject;

public class BootstrapInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        BindProgressService();
    }

    private void BindProgressService()
    {
        Container.Bind<IProgressService>().To<ProgressService>().AsSingle().NonLazy();
    }
}