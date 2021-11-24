using RootMotion.Dynamics;
using System.Collections;
using Tzaik.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.General
{
    public class HealthScript : MonoBehaviour
    {
        #region Fields
          
        [SerializeField] LayerMask layerMask; 
        [SerializeField] bool checkDeath; 
        [SerializeField] UnityEvent deathEvent; 
        [SerializeField] UnityEvent damagedEvent; 
        [SerializeField] DamageEvent damageEvent;
        [SerializeField] HealEvent healEvent; 
        [SerializeField] bool HasParentHealthScript = false; 
        [SerializeField] HealthScript parentHealthScript;  
        [SerializeField] bool isEnemy; 
        [SerializeField] float cooldownTime; 
        [SerializeField] bool detectCollisions; 
        [SerializeField] float stunTime;
        [SerializeField] float invicibilityTime;
        [SerializeField] PuppetMaster puppetMaster;

        [System.Serializable]
        public class DamageEvent : UnityEvent<float> { } 
        [System.Serializable]
        public class HealEvent : UnityEvent<float> { }

        public bool Damaged;
        float currentCooldown;
        bool isDead;
        bool canBeDamaged;

        #endregion

        #region Properties
        public float MaxHealth;
        public Vector3 ForceRecieved { get; set; }
        public ForceMode ForceTypeReceived { get; set; }
        public float CurrentHealth { get; private set; }
        public float StunTime => stunTime; 
        public UnityEvent DeathEvent  => deathEvent;
        public bool CanBeDamaged => canBeDamaged;

        #endregion

        #region Unity Methods
        private void Start()
        {
            currentCooldown = cooldownTime;
            CurrentHealth = MaxHealth;
            isDead = false;
            canBeDamaged = true;
        }
        public void Update()
        {
            if (checkDeath)
                CheckHealth(); 
            HealthChangeCooldown();
        }
        #endregion

        #region Methods
        public bool AddHealth(float h)
        {
            if (CurrentHealth + h <= MaxHealth)
            {
                healEvent.Invoke(MaxHealth/CurrentHealth);
                CurrentHealth++; return true;
            }
            else return false;
        }

        public void Damage(float h)
        {   
            if(canBeDamaged)
            { 
                CurrentHealth -= h;

                damagedEvent.Invoke();
                damageEvent.Invoke(MaxHealth / CurrentHealth);

                if (HasParentHealthScript)
                    parentHealthScript.Damage(h);
                else
                    Damaged = true;
            }
        }   

        public void CheckHealth()
        {
            if (CurrentHealth <= 0)
            { 
                deathEvent.Invoke();
                isDead = true;
            }
        }   
        public void AddKill()
        {
            if(isEnemy && !isDead)
            {
                GameManager.Instance.Player.GetComponent<PlayerSpecial>().CurrentSpecial += 10;
                GameManager.Instance.AddKill();
            }
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (detectCollisions)
            {
                if (layerMask == (layerMask | (1 << hit.gameObject.layer)))
                {
                    if (HealthCooldownCheck())
                    {
                        Damage(1);
                        currentCooldown = cooldownTime;
                    }

                }
            }
        }
        public void RemoveHealth(float h)
            => CurrentHealth -= h;
        private void OnCollisionEnter(Collision collision)
        {
            if (detectCollisions)
            {
                if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
                {
                    if (HealthCooldownCheck())
                    {
                        Damage(1);
                        currentCooldown = cooldownTime;
                    }
                }
            }
        }
        public bool HealthCooldownCheck() 
            => currentCooldown <= 0;
        public void HealthChangeCooldown() 
            => currentCooldown -= currentCooldown > 0 ? Time.deltaTime : 0; 
        public void PerformDamageByTime(float damage, float time, float rate)
            => StartCoroutine(DamageByTime(damage, time, rate));
        IEnumerator DamageByTime(float damage, float time, float rate)
        {
            float totalTimer = 0;
            float damageTimer = 0;
            while(totalTimer < time)
            {
                totalTimer += Time.deltaTime;
                damageTimer += Time.deltaTime;
                if (damageTimer < rate)
                    Damage(damage);
                else
                    damageTimer = 0;
                yield return null;
            }
        }
        public void SetStunned() => StartCoroutine(StunnedCoroutine());

        IEnumerator StunnedCoroutine()
        {
            canBeDamaged = false;
            yield return new WaitForSeconds(invicibilityTime);
            canBeDamaged = true;
        }

        public void SetDamagable(bool can) => canBeDamaged = can;

        #endregion
    }
}
