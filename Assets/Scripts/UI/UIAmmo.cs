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
        Weapon weapon; 

        public void GetCurrentWeapon(Weapon w) 
            => weapon = w;
        public void ShowAmmo()
            => ammoImg.fillAmount = (weapon != null) ? weapon.WeaponAmmo.TotalAmmo / weapon.WeaponAmmo.MaxAmmo : 1;

    }
}
