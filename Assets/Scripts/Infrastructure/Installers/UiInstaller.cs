using Zenject;

namespace Installers
{
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
}