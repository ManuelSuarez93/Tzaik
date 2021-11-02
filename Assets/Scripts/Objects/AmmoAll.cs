using System; 
using Tzaik.General;
using Tzaik.Items.Misc;
using Tzaik.Items.Weapons;
using Tzaik.Player;
using UnityEngine;

namespace Tzaik.Items.Pickups
{
    public class AmmoAll : TriggerCollisionObject
    {
        [SerializeField] WeaponType type;
        [SerializeField] int ammoBlowgun;
        [SerializeField] int ammoBoleadora;
        [SerializeField] int ammoAltatl;
        [SerializeField] int ammoSling;
        PlayerInventory inv;

        private void Start() => inv = GameManager.Instance.Player.GetComponent<PlayerController>().Inventory;
        public override bool DoActionReturnBool(GameObject obj = null)
        {
            foreach(Weapon w in inv.Weapons)
                switch(w.Type)
                {
                    case WeaponType.Boleadora:
                        w.WeaponAmmo.AddAmmo(ammoBoleadora);
                        break;
                    case WeaponType.Sling:
                        w.WeaponAmmo.AddAmmo(ammoSling);
                        break;
                    case WeaponType.Atlatl:
                        w.WeaponAmmo.AddAmmo(ammoAltatl);
                        break;
                    case WeaponType.Blowgun:
                        w.WeaponAmmo.AddAmmo(ammoBlowgun);
                        break;
                    default: break;
                }
            return true;
        }
        public int GetIndex() => Array.FindIndex(inv.Weapons, element => element != null && element.Type == type);
    }
}
