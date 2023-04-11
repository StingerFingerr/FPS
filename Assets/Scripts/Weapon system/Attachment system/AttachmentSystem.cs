using UI.Game.Inventory.Model;
using UnityEngine;

namespace Attachment_system
{
    public class AttachmentSystem: MonoBehaviour
    {
        public BarrelModuleModelChanger barrel;
        public MagazineModuleModelChanger magazine;

        private BaseBarrelModule _barrelModule;
        private BaseMagazineModule _magazineModule;
        
        #region Attachments

        public void SetModule(SecondaryInventoryItemType type)
        {
            switch (type)
            {
                case SecondaryInventoryItemType.Silencer:
                    SetBarrelModule(new SilencerModule(.8f));
                    break;
                case SecondaryInventoryItemType.Compensator:
                    SetBarrelModule(new CompensatorModule(1.3f));
                    break;
                case SecondaryInventoryItemType.ExtendedMagazine:
                    SetMagazineModule(new ExtendedMagazineModule(1.5f));
                    break;
            }
        }

        public void RemoveModule(SecondaryInventoryItemType type)
        {
            switch (type)
            {
                case SecondaryInventoryItemType.Silencer:
                case SecondaryInventoryItemType.Compensator:
                    RemoveBarrelModule();
                    break;
                case SecondaryInventoryItemType.ExtendedMagazine:
                    RemoveMagazineModule();
                    break;
            }
        }
        
        #region Barrel

        private void SetBarrelModule(BaseBarrelModule barrelModule)
        {
            _barrelModule = barrelModule;
            barrel.SetModelFor(barrelModule.Type);
        }

        private void RemoveBarrelModule()
        {
            _barrelModule = null;
            barrel.ResetModel();
        }

        #endregion

        #region Magazine

        private void SetMagazineModule(BaseMagazineModule magazineModule)
        {
            _magazineModule = magazineModule;
            magazine.SetModelFor(magazineModule.Type);
        }

        private void RemoveMagazineModule()
        {
            _magazineModule = null;
            magazine.ResetModel();
        }

        #endregion

        #endregion

        #region Overrides

        public float OverrideReloadingTime(float reloadingTime)
        {
            if (_magazineModule is null)
                return reloadingTime;

            return _magazineModule.OverrideReloadingTime(reloadingTime);
        }

        public int OverrideMagazineCapacity(int capacity)
        {
            if (_magazineModule is null)
                return capacity;

            return _magazineModule.OverrideMagazineCapacity(capacity);
        }
        
        public int OverrideDamage(int damage)
        {
            if (_barrelModule is null)
                return damage;

            return _barrelModule.OverrideDamage(damage);
        }

        public AudioClip OverrideShotSound(AudioClip defaultClip) => 
            barrel.OverrideShotSound(defaultClip);

        #endregion
        
        
    }
}