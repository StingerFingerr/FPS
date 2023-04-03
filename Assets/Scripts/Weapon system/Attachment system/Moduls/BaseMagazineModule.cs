
namespace Attachment_system
{
    public abstract class BaseMagazineModule
    {
        public abstract MagazineModuleType Type { get; }
        
        public abstract int OverrideMagazineCapacity(int capacity);
        public abstract float OverrideReloadingTime(float reloading);
    }
}