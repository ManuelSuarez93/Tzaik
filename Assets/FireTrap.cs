using System.Collections;
using System.Collections.Generic;
using Tzaik.General;
using UnityEngine;

namespace Tzaik
{
    public class FireTrap : MonoBehaviour
    {
        [SerializeField] float trapCooldown;
        [SerializeField] float damage;
        [SerializeField] float force;
        [SerializeField] Animator animator;
        [SerializeField] string triggerName;
        float traptimer;
        void Start() => traptimer = Time.time;

        // Update is called once per frame
        void Update()
        {
            if (traptimer + trapCooldown <= Time.time)
            {
                animator.SetTrigger(triggerName);
                traptimer = Time.time;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var player = collision.collider.gameObject;
            if(player.CompareTag("Player"))
            {
                player.GetComponent<HealthScript>().Damage(damage);
                player.GetComponentInParent<Rigidbody>().AddForce(player.transform.forward * -1 * force, ForceMode.Impulse);
            }
        }
    }
}
