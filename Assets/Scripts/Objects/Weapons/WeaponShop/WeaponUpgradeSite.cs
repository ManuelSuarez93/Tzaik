using Tzaik.Player; 
using System.Collections.Generic; 
using UnityEngine; 
using System.Linq;
using System;
using Tzaik.General; 
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tzaik.Items.Weapons
{
    [Serializable]
    public enum UpgradeType
    {
        StandardUpgrade1, StandardUpgrade2, SpecialUpgrade
    }
    public class WeaponUpgradeSite : MonoBehaviour, IInteractable
    {
        #region Fields
        [SerializeField] GameObject shopUI;
        [SerializeField] PlayerInventory inventory;
        [SerializeField] Weapon weapon; 
        [SerializeField] int selectedSlot;
        [SerializeField] UpgradeButton addButton;
        [SerializeField] UpgradeButton removeButton;
        [SerializeField] UpgradeWeaponSlotButton selectButton;
        [SerializeField] WeaponModelsUI modelsUI;
        [SerializeField] TextMesh slotSelectorText;
        [SerializeField] TextMesh weaponDescription; 

        [SerializeField] UnityEvent enableEvent;
        [SerializeField] UnityEvent disableEvent;
        bool shopEnabled;
        PlayerCoins coins;
        string description;

        #endregion
        private void Start()
        {
            if (GameManager.Instance.Player.GetComponent<PlayerController>().Inventory.CheckForWeaponInInventory(weapon.Type))
            {
                weapon = GameManager.Instance.Player.GetComponent<PlayerController>().
                        Inventory.Weapons.Where(x => x?.Type == weapon.Type).FirstOrDefault();
            }
            else
            {
                weapon = Instantiate(weapon);
                weapon.Initialize();
                weapon.gameObject.SetActive(false);
            } 
            inventory = GameManager.Instance.Player.GetComponent<PlayerController>().Inventory;
            shopEnabled = false;
            EnableShop(shopEnabled); 
        }
         

        #region Methods
        void EnableShop(bool e)
        {
            if (e)
                enableEvent.Invoke();
            else
                disableEvent.Invoke();
        }
        public void SetSlot(int i) => selectedSlot = i;
        public void AddWeaponInShopToInventory()
        {
            int oldSlot = weapon.WeaponUI.InventorySlot;
            weapon.WeaponUI.InventorySlot = selectedSlot;
            weapon.WeaponUI.HasInventorySlot = true;
            if (inventory.Weapons[selectedSlot] == null)
            {
                inventory.AddItem(weapon, selectedSlot, oldSlot);
                slotSelectorText.text = $"{weapon.Type} added to {selectedSlot}";
            }
            else
                slotSelectorText.text = $"{selectedSlot} is occupied"; 
        }
 
 

        public void SaveWeapon()
            => weapon.SaveWeaponObject();
        public void RemoveFromInventory()
        {
            inventory.RemoveFromInventory(weapon, weapon.WeaponUI.InventorySlot, transform); 
            slotSelectorText.text = $"{weapon.Type} removed from {weapon.WeaponUI.InventorySlot}";
        }
        public void DoInteraction()
        {
            shopEnabled = !shopEnabled;
            EnableShop(shopEnabled); 
            SetUIButtonOnClickEvent();
            SetWeaponDescription();
            InputManager.ShowMouse(shopEnabled);
        }
         
        private void SetWeaponDescription()
        {
            weaponDescription.text =
                $"Name = {weapon.Type} \n" +
                $"Attack rate = {weapon.WeaponShootRate} \n" +
                $"Special =\n" +
                //$"Special cost ={weapon.Special.TotalSpecial}" +
                $"Damage = {weapon.WeaponAttack.BaseDamage + weapon.WeaponAttack.AdditionalDamage}\n" +
                $"Attack Force = {weapon.WeaponAttack.BaseForce + weapon.WeaponAttack.AdditionalForce}\n";

            weaponDescription.text += weapon.WeaponAmmo.UsesAmmo ?
               $"Max ammo = {weapon.WeaponAmmo.MaxAmmo}\n" :
               "Max ammo = N/A";
            weaponDescription.text += weapon.WeaponAttack.Projectile != null ?
                $"Projectile speed = {weapon.WeaponAttack.BaseSpeed + weapon.WeaponAttack.AdditionalForce}\n" : 
                "Projectile speed = N/A"; 
        }
 

        private void SetUIButtonOnClickEvent()
        {
            if(shopEnabled)
            {  
                addButton.ClickEvent.AddListener(() => AddWeaponInShopToInventory());
                removeButton.ClickEvent.AddListener(() => RemoveFromInventory()); 
                selectButton.SetSlotEvent.AddListener(() => SetSlot(selectButton.CurrentSlot));
                selectButton.Type = weapon.Type;
                modelsUI.SetWeaponModel(weapon.Type);
            }
            else
            {  
                addButton.ClickEvent.RemoveAllListeners();
                removeButton.ClickEvent.RemoveAllListeners();
            }
        }
        #endregion
    }

}
