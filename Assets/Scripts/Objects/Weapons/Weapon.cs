using Tzaik.Player;
using UnityEngine;
using UnityEngine.Events;
using Tzaik.LevelObjects;
using Tzaik.SaveSystem;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Tzaik.Items.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        #region Fields
        [Header("Weapon properties")]
        [SerializeField] protected float shootRate;
        [SerializeField] protected bool isRanged;
        [SerializeField] protected UnityEvent attackEvent;
        [SerializeField] protected UnityEvent hitEvent;
        [SerializeField] protected Animator animator;
        [SerializeField] protected WeaponType type; 
        protected float rate;

        [Header("Weapon Ammo")] 
        [SerializeField] protected WeaponAmmo ammo;
        [Header("Weapon Attack")]
        [SerializeField] protected WeaponAttack attack;
        [Header("Weapon UI")]
        [SerializeField] protected WeaponUI ui; 
        [Header("Weapon Effects")]
        [SerializeField] protected WeaponEffects effects; 
        #endregion 

        #region Properties
        public WeaponAmmo WeaponAmmo => ammo; 
        public WeaponUI WeaponUI => ui;
        public WeaponAttack WeaponAttack => attack; 
        public float WeaponShootRate => shootRate;
        public WeaponType Type  => type; 

        #endregion

        #region Unity Methods  
        void Update()
        {
            AttackRateTimer(); 
            effects.WeaponSway();
        }

        #endregion

        #region Weapon Functions
        protected virtual void Attack()
        {  
            if (isRanged)
                attack.InstantiateProjectile(attack.Projectile);

            if (ammo.UsesAmmo)
                ammo.RemoveAmmo(ammo.UsedAmmo);

            attackEvent.Invoke();
            rate = 0;
        }  
        public virtual void AttackActionCheck()
        { 
            if (AttackConidition)
                Attack(); 
        }

        public virtual void PerformAttack()
        { 
            if (AttackConidition)
                Attack();
        }
        public virtual void ReleaseShot() { }
        #endregion 

        #region Weapon shoot rate
        public void AttackRateTimer() => rate = rate < shootRate ? rate + Time.deltaTime : shootRate;
        public void SetRate(int r) => rate = r;
         
        #endregion

        protected virtual bool AttackConidition
            => rate == shootRate && ammo.TotalAmmo > 0; 
 
        public void Initialize(Transform handPoint = null)
        {
            if (handPoint != null)
            {
                transform.parent = handPoint;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
            rate = shootRate; 
            effects.Initialize();
            ammo.Initialize();
        } 
        public void SaveWeaponObject()
        { 
            var saveWeaponObject = new SaveWeaponObject
            {
                name = type.ToString(), 
                slot = ui.InventorySlot
            };
                
            SaveManager.Save(saveWeaponObject, $"Weapon-{type}"); 
        } 
        public void LoadWeaponObject()
        {
            var loadWeaponObject = new SaveWeaponObject(); 
            loadWeaponObject = SaveManager.Load(loadWeaponObject, $"Weapon-{type}");
            
            if (loadWeaponObject != null)
            {  
                ui.InventorySlot = loadWeaponObject.slot; 
            }
            else
                Debug.LogWarning($"<color=red>No weapon has been loaded for {name} </color>");
        }   
        public void Shake() => effects.Shake(); 
        public void Move() => effects.Move();
    }

}
