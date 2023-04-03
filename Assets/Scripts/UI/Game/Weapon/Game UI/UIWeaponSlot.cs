using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Game_UI
{
    public class UIWeaponSlot: MonoBehaviour
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