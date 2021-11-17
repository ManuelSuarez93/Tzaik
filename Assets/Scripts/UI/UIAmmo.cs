using Tzaik.Items.Weapons;
using Tzaik.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik
{
    [System.Serializable]
    public class UIAmmo
    { 
        [SerializeField] Text ammoText;
        [SerializeField] Image ammoImg; 
         
        public void ShowAmmo(Weapon w)
            => ammoImg.fillAmount = (w != null) ? w.WeaponAmmo.TotalAmmo / w.WeaponAmmo.MaxAmmo : 1;

    }
}
