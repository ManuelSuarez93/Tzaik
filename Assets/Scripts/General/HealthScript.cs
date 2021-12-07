using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] Renderer renderer;
        [SerializeField] [GradientUsage(true)] Gradient gradient;
        [SerializeField] float outlineThicknessOnLook;
        [SerializeField] float outlineThicknessNormal; 
        [SerializeField] List<GameObject> objectToInstance;

        [System.Serializable]
        public class DamageEvent : UnityEvent<float> { } 
        [System.Serializable]
        public class HealEvent : UnityEvent<float> { }

        public bool Damaged;
        float currentCooldown;
        bool isDead;
        bool canBeDamaged; 
        bool isBeingLooked;

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
        #endregion

        #region Class Methods
        public bool AddHealth(float h)
        {
            if (CurrentHealth + h <= MaxHealth)
            {
                healEvent.Invoke(MaxHealth/CurrentHealth);
                CurrentHealth += h; return true;
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

                UpdateOutlineDamage();
            } 
        }   
        public void UpdateOutlineDamage()
        {
            if(renderer == null) return;

            renderer.material.SetColor("_OutlineColor", gradient.Evaluate(CurrentHealth/MaxHealth));
        }

        public void SetOutlineThickness(bool set)
        {
            if(renderer == null) return; 
            renderer.material.SetFloat("_OutlineThickness", set ? outlineThicknessOnLook : outlineThicknessNormal); 
        }
        private void SetColorNotDamagable(bool damagable)
        {
            if(renderer == null) return; 

            if(!damagable)
                renderer.material.SetColor("_OutlineColor", Color.gray);
            else
                UpdateOutlineDamage(); 
                    
        }
        private void CheckHealth()
        {
            if (CurrentHealth <= 0 && !isDead)
            { 
                deathEvent.Invoke();
                isDead = true;
                Debug.Log($"{name} is dead", this);
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

        public void Instantiate(int id) => Instantiate(objectToInstance[id], transform.position, Quaternion.identity);
        public void InstatiateDroppables()
        {
            foreach (GameObject go in objectToInstance)
                Instantiate(go, DroppablePosition(), Quaternion.identity);
        }

        private Vector3 DroppablePosition()
        {
            var newPos = transform.position + Random.insideUnitSphere * 3f;
            return new Vector3(newPos.x, transform.position.y, newPos.z);
        }

        public void RemoveHealth(float h)
            => CurrentHealth -= h;
        public bool HealthCooldownCheck() 
            => currentCooldown <= 0;
        public void HealthChangeCooldown() 
            => currentCooldown -= currentCooldown > 0 ? Time.deltaTime : 0; 
        public void PerformDamageByTime(float damage, float time, float rate)
            => StartCoroutine(DamageByTime(damage, time, rate));

        public void SetDamagable(bool can) 
        {
            canBeDamaged = can;
            SetColorNotDamagable(can);
        }         
        public void SetStunned() => StartCoroutine(StunnedCoroutine());
        #endregion

        #region Coroutines
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
        IEnumerator StunnedCoroutine()
        {
            canBeDamaged = false;
            yield return new WaitForSeconds(invicibilityTime);
            canBeDamaged = true;
        }
        #endregion

    }
}
