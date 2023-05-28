namespace Attachment_system
{
    public class SilencerModule: BaseBarrelModule
    {
        private float damageMultiplier = 1;

        public SilencerModule (float damageMultiplier) => 
            this.damageMultiplier = damageMultiplier;

        public override BarrelModuleType Type => BarrelModuleType.Silencer;

        public override int OverrideDamage(int damage) => 
            (int) (damage * damageMultiplier);
    }
}