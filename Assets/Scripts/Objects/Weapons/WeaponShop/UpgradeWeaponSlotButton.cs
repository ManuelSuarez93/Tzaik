using System.Collections.Generic;
using Tzaik.General; 
using Tzaik.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Items.Weapons
{
    public class UpgradeWeaponSlotButton : UpgradeButton
    {
        [SerializeField] List<GameObject> slots;
        [SerializeField] Color selectedColor;
        [SerializeField] Color unSelectedColor;
        [SerializeField] Color occupiedColor;
        [SerializeField] Color occupiedSelectedColor;
        [SerializeField] Color occupiedColorThisWeapon; 
        [SerializeField] Color occupiedSelectedColorThisWeapon;
        [SerializeField] UnityEvent setSlotEvent; 
        int currentSlot = 0;
        PlayerInventory inv;

        public UnityEvent SetSlotEvent { get => setSlotEvent; set => setSlotEvent = value; }
        public WeaponType Type {get;set;}
        public int CurrentSlot => currentSlot;
        private void Start()
        {
            inv = GameManager.Instance.Player.GetComponent<PlayerController>().Inventory;
            SetSlot();
        }

        public void NextSlot()
        {
            if (currentSlot < slots.Count -1)
                currentSlot++;
            else
                currentSlot = 0;
            SetSlot();
        }

        public void SetSlot()
        {
            for (int i = 0; i < slots.Count; i++)
                slots[i].GetComponent<Renderer>().material.color =
                    IsOccupiedSelectedAndIsThisWeaponColor(i) ? occupiedSelectedColorThisWeapon :
                    IsOccupiedSelectedColor(i) ? occupiedSelectedColor :
                    IsOccupiedAndIsThisWeaponColor(i) ? occupiedColorThisWeapon :
                    IsOccupied(i) ? occupiedColor :
                    IsNotOccupiedAndSelected(i) ? selectedColor:
                    unSelectedColor;

            SetSlotEvent.Invoke();
        }

        private bool IsOccupiedSelectedAndIsThisWeaponColor(int i) =>
            i == currentSlot && inv.Weapons[i] != null && inv.Weapons[i].Type == Type;
        private bool IsOccupiedSelectedColor(int i) =>
            i == currentSlot && inv.Weapons[i] != null; 
        private bool IsOccupiedAndIsThisWeaponColor(int i) =>
            inv.Weapons[i] != null && inv.Weapons[i].Type == Type; 
        private bool IsOccupied(int i) => 
            inv.Weapons[i] != null;
        private bool IsNotOccupiedAndSelected(int i) =>
            i == currentSlot && inv.Weapons[i] == null;
        public void PreviousSlot()
        {
            if (currentSlot > 0)
                currentSlot--;
            else
                currentSlot = slots.Count - 1;
            SetSlot();
        }
    }
}
