using UnityEngine;

namespace Tzaik.Items.Weapons
{
    [System.Serializable]
    public class WeaponAmmo
    {
        #region Fields
        [SerializeField] int maxAmmo;
        [SerializeField] int usedAmmo;
        [SerializeField] int totalAmmo;
        [SerializeField] bool usesAmmo;
        #endregion
        #region Properties
        public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; } 
        public int TotalAmmo { get => totalAmmo; set => totalAmmo = value; } 
        public int UsedAmmo { get => usedAmmo; }
        public bool UsesAmmo { get => usesAmmo; set => usesAmmo = value; }
        #endregion
        #region Ammo Functionality

        public void Initialize() => totalAmmo = maxAmmo;
        public bool AddAmmo(int ammo)
        {
            if (CheckAmmo) 
                totalAmmo += ammo;  
            else
                return false;
            return true;
        } 
        public void RemoveAmmo(int ammo)
            => totalAmmo -= ammo;
        public bool CheckAmmo
            => totalAmmo != maxAmmo;  
          
        #endregion
    }
}
