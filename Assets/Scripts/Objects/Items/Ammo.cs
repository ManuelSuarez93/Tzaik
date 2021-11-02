using Tzaik.Player;
using Tzaik.Items.Weapons;
using System;
using UnityEngine;
using Tzaik.General;

namespace Tzaik.Items.Misc
{
    public class Ammo : TriggerCollisionObject
    {
        [SerializeField] WeaponType type;
        [SerializeField] int ammoAmount;
        PlayerInventory inv;

        private void Start() => inv = GameManager.Instance.Player.GetComponent<PlayerController>().Inventory;
        public override bool DoActionReturnBool(GameObject obj = null) => inv.Weapons[GetIndex()].WeaponAmmo.AddAmmo(ammoAmount); 
        public int GetIndex() => Array.FindIndex(inv.Weapons, element => element != null && element.Type == type);
    }
}
