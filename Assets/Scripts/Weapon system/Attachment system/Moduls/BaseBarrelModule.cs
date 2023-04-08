namespace Attachment_system
{
    public abstract class BaseBarrelModule
    {
        public abstract BarrelModuleType Type { get; }
        public abstract int OverrideDamage(int damage);
    }
}