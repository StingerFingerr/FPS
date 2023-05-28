namespace Attachment_system
{
    public class CompensatorModule: BaseBarrelModule
    {
        public override BarrelModuleType Type => BarrelModuleType.Сompensator;

        private float _damageMultiplier;

        public CompensatorModule (float damageMultiplier) => 
            _damageMultiplier = damageMultiplier;

        public override int OverrideDamage(int damage) => 
            (int) (damage * _damageMultiplier);
    }
}