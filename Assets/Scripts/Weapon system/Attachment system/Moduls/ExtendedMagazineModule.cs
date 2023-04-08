
namespace Attachment_system
{
    public class ExtendedMagazineModule: BaseMagazineModule
    {
        private float capacityMultiplier = 1.5f;

        public ExtendedMagazineModule (float capacityMultiplier) => 
            this.capacityMultiplier = capacityMultiplier;

        public override MagazineModuleType Type => MagazineModuleType.ExtendedMagazine;

        public override int OverrideMagazineCapacity(int capacity) => 
            (int) (capacity * capacityMultiplier);

        public override float OverrideReloadingTime(float reloading) => 
            reloading;
    }
}