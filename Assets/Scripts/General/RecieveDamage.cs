using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.General
{
    public class RecieveDamage : MonoBehaviour
    {
        [SerializeField] HealthScript healthScript;
        [SerializeField] UnityEvent damageEvent;
        [SerializeField] UnityEvent destroyEvent;
        [SerializeField] int maxDamage;
        int currentDamageTaken;

        public void Damage(float damage)
        {
            healthScript.Damage(damage);
            currentDamageTaken++;
            damageEvent.Invoke();
            if(currentDamageTaken == maxDamage)
            {
                Destroy(gameObject);
                destroyEvent.Invoke(); 
            }
        }

         
    }
}
