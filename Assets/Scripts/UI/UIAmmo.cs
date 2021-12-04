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
            => ammoImg.fillAmount = (w != null) ? (float)w.WeaponAmmo.TotalAmmo / (float)w.WeaponAmmo.MaxAmmo  : 1;

    }
}
