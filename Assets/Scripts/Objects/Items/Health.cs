using Tzaik.General;
using UnityEngine;

namespace Tzaik.Items.Misc
{
    public class Health : TriggerCollisionObject
    {
        [SerializeField] float healthAmount;
        HealthScript hs;
        
        private void Start() => hs = GameManager.Instance.Player.GetComponent<HealthScript>();
        public override bool DoActionReturnBool(GameObject obj = null) => obj.GetComponent<HealthScript>().AddHealth(healthAmount);
 
    }
}
