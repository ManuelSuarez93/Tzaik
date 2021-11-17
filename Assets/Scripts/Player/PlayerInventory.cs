using System.Linq;
using System.Collections.Generic; 
using Tzaik.Items.Weapons;
using Tzaik.UI;
using UnityEngine; 
using System; 
using Tzaik.General;

namespace Tzaik.Player
{
    [System.Serializable]
    public class PlayerInventory
    {
        #region Properties
        [SerializeField] Transform inventoryPoint; 
        [SerializeField] Weapon[] weapons;
        [SerializeField] ListOfWeapons weaponList; 
        Weapon currentWeapon; 


        public Weapon[] Weapons  => weapons; 
        public Weapon CurrentWeapon  => currentWeapon;   
        #endregion

        #region Methods

        public void SetWeapons(Weapon[] wepArray) 
            =>  weapons = wepArray;

        public void AddItem(Weapon weapon, int newSlot, int oldSlot = -1)
        { 
            if (CheckForWeaponInInventory(weapon.Type))
            {
                ExistingWeapon(weapon, newSlot, oldSlot);
                return;
            }
            
            weapon.Initialize(inventoryPoint);
            AddToInventory(newSlot, weapon);
            AddWeaponToUISlot(weapon);

            if (currentWeapon == null) ActivateItem(weapon.WeaponUI.InventorySlot); 
        } 
        private void ExistingWeapon(Weapon weapon, int newSlot, int oldSlot)
        { 
            if (weapon != null && weapons.Contains(weapon))
            {
                RemoveFromInventory(weapon, oldSlot);
                weapon.WeaponUI.InventorySlot = newSlot;
                AddToInventory(newSlot, weapon);
                AddWeaponToUISlot(weapon);
            }
        }

        void AddWeaponToUISlot(Weapon w)
            => UIManager.Instance.UIInv.AddSlot(w.WeaponUI.InventorySlot, w.WeaponUI.Sprite); 
        void RemoveWeaponfromUISlot(int slot)
            => UIManager.Instance.UIInv.RemoveSlot(slot);

        void ActivateItem(int w)
        {  
            if(w == -1) 
                currentWeapon = null; 
            else
            { 
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i] != null)
                    {
                        weapons[i].gameObject.SetActive(i == w);
                    }
                }
                currentWeapon = weapons[w];
            }
        }
        Weapon InstantiateItem(GameObject w)
        {
            w = UnityEngine.Object.Instantiate(w,inventoryPoint); 
            w.transform.localPosition = Vector3.zero;
            w.transform.localRotation = Quaternion.identity;
            w.gameObject.SetActive(false);
            return w.GetComponent<Weapon>();
        }
        public void SelectItemScrollWheel()
        {

        }
        public void SelectItem(int num, bool isSwitch = false)
        {  
            if(isSwitch)
            {
                if (num >= 0 && currentWeapon.WeaponUI.InventorySlot < weapons.Length)
                    num = currentWeapon.WeaponUI.InventorySlot++;
                else if (num > 0 && currentWeapon.WeaponUI.InventorySlot >= weapons.Length)
                    num = 0;
                else if (num < 0 && currentWeapon.WeaponUI.InventorySlot > 0)
                    num = currentWeapon.WeaponUI.InventorySlot--;
                else if (num < 0 && currentWeapon.WeaponUI.InventorySlot <= 0)
                    num = weapons.Length - 1; 
            }

            if (num != -1 && weapons.Length > num)
                ActivateItem(num); 

            if (num != -1)
                UIManager.Instance.UIInv.DoWheel(num); 
        }
        void AddToInventory(int s, Weapon o)
        {
            if(s != -1) 
                weapons[s] = o;
            else
            {
                for(int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i] == null)
                    { 
                        o.WeaponUI.HasInventorySlot = true;
                        o.WeaponUI.InventorySlot = i;
                        weapons[i] = o;
                        return;
                    }
                }
            }
        } 
        public void RemoveFromInventory(Weapon o, int oldSlot, Transform shop = null)
        {
            if (shop != null)
            { 
                o.transform.parent = shop;
                o.gameObject.SetActive(false);
            }

            RemoveWeaponfromUISlot(oldSlot); 
            o.WeaponUI.InventorySlot = -1;
            weapons[Array.IndexOf(weapons, o)] = null;
            ActivateItem(-1);

            GameManager.Instance.Save();
        }  
        public Weapon CheckForWeaponInInventory(WeaponType type)
            => weapons.FirstOrDefault(x => x != null && x.Type == type);  
        public void LoadInventory(List<WeaponType> weapons)
        {
            if(weapons.Count > 0) 
                foreach (WeaponType w in weapons)
                {
                    var weapon = InstantiateItem(weaponList.weapon.Where(x => x.Type == w).FirstOrDefault().gameObject);
                    weapon.LoadWeaponObject();
                    weapon.Initialize();
                    AddToInventory(weapon.WeaponUI.InventorySlot, weapon);
                    AddWeaponToUISlot(weapon);
                } 

        } 
        public void PerformAttack() => currentWeapon?.PerformAttack();
        public void PerformRelease() => currentWeapon?.ReleaseShot(); 
        public void PerformSpecial(PlayerSpecial special) => currentWeapon?.PerformSpecial(special);
        
        #endregion
    }

    
}
