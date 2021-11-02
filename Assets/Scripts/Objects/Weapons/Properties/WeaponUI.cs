 using UnityEngine;

namespace Tzaik.Items.Weapons
{
    [System.Serializable]
    public class WeaponUI
    {

        [SerializeField] protected int inventorySlot;
        [SerializeField] private bool hasInventorySlot;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected string text; 
        public Sprite Sprite  => sprite; 
        public string Text  => text;

        public int InventorySlot { get => inventorySlot; set => inventorySlot = value; }
        public bool HasInventorySlot { get => hasInventorySlot; set => hasInventorySlot = value; }
    }
}
