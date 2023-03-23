using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class WeaponSlotView: MonoBehaviour
    {
        public Image weaponIcon;
        
        public void SetView(Sprite newIcon)
        {
            if (newIcon is null)
                weaponIcon.enabled = false;
            else
                weaponIcon.enabled = true;
            
            weaponIcon.sprite = newIcon;
        }
    }
}