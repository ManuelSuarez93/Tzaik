using UnityEngine;
using Tzaik.Items.Misc;
using Tzaik.General;

namespace Tzaik.Items.Pickups
{
    public class PickupItem : Spawnable
    {
        [SerializeField] TriggerCollisionObject interactable;
        [SerializeField] LayerMask layerMask;
         
        private void OnTriggerEnter(Collider other)
        {
            if (layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                interactable.DoActionReturnBool();
            }
        }
    }
}
