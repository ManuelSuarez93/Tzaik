using UnityEngine;
using Tzaik.UI;
using Tzaik.Player;
using Tzaik.Items.Weapons;
using Tzaik.General;

namespace Tzaik.Items.Pickups
{
    [RequireComponent(typeof(Collider))]
    public class PickupWeapon : MonoBehaviour
    {
        [SerializeField] WeaponType type;
        [SerializeField] string pickUpText;
         
        private PlayerInventory inv;

        void Start() => inv = GameManager.Instance.Player.GetComponent<PlayerController>().Inventory;

        //void OnTriggerEnter(Collider other)
        //{
        //    if(other.CompareTag("Player"))
        //    {
        //        inv.AddItem(type);
        //        Destroy(gameObject);
        //    }
        //}
         
    }
}
