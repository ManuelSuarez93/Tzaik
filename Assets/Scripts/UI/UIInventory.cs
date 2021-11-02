using System.Collections.Generic;
using Tzaik.Player;
using UnityEngine;

namespace Tzaik.UI
{
    [System.Serializable]
    public class UIInventory
    {
        [SerializeField] List<UIInventorySlot> slots;
        [SerializeField] WeaponWheel wheel;
        public PlayerInventory inventory { private get; set; }

        public void AddSlot(int i, Sprite img)
            => slots[i].SetSlotImage(img);
        public void RemoveSlot(int i)
            => slots[i].SetSlotImage();

        public void SelectedImage(int i, bool isSelected)
            => slots[i].Selection(isSelected);

        public void DoWheel(int i)
        {
            switch (i)
            {
                case 0:
                    wheel.DoChangeWeapon(0); return;
                case 1:
                    wheel.DoChangeWeapon(-120); return;
                case 2:
                    wheel.DoChangeWeapon(-240); return;
            }
        }

    }
}
