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
        [SerializeField] protected UnityEvent specialAttackEvent;
        [SerializeField] protected Animator animator;
        [SerializeField] protected WeaponType type; 
        protected float rate;

        [Header("Weapon Ammo")] 
        [SerializeField] protected WeaponAmmo ammo;
        [Header("Weapon Attack")]
        [SerializeField] protected WeaponAttack attack;
        [Header("Weapon UI")]
        [SerializeField] protected WeaponUI ui;
        [Header("Weapon Upgrades")]
        [SerializeField] protected WeaponUpgrades upgrades;
        [Header("Weapon Effects")]
        [SerializeField] protected WeaponEffects effects;
        [Header("Weapon Special")]
        [SerializeField] protected float specialTime;
        [SerializeField] protected float specialCost;
        protected float specialRate;
        #endregion 

        #region Properties
        public WeaponAmmo WeaponAmmo => ammo; 
        public WeaponUI WeaponUI => ui;
        public WeaponAttack WeaponAttack => attack;
        public WeaponUpgrades  Upgrades => upgrades;
        public float WeaponShootRate => shootRate;
        public WeaponType Type  => type;
        public float SpecialRate => specialRate / specialTime;

        #endregion

        #region Unity Methods  
        void Update()
        {
            AttackRateTimer();
            SpecialRateTimer();
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
        
        private void SpecialRateTimer() => specialRate = specialRate < specialTime ? specialRate + Time.deltaTime : specialRate;
        #endregion

        protected virtual bool AttackConidition
            => rate == shootRate && ammo.TotalAmmo > 0; 

        public string CheckAndPerformUpgrade(int upgradeType, PlayerCoins coins)
        {
            var type = (UpgradeType)upgradeType;
            if (upgrades.UpgradesLevel.ContainsKey(type))
            {
                var level = upgrades.UpgradesLevel[type] + 1;
                var upgrade = upgrades.GetUpgrade(level, type);
                if (coins.CoinsAmount[upgrade.CoinType] < upgrade.CoinCost)
                    return "Error";
                else
                    upgrades.UpgradesLevel[type]++;

                DoUpgrade(upgrade);
                SaveWeaponObject();
                return "";
            } 
            else
                return $"{upgradeType} does not exist";
        }
        protected virtual void DoUpgrade(Upgrade upg)
        {
            if (upg == null) return;  
             
            ammo.MaxAmmo += upg.MaxAmmoUpgrade;
            shootRate += upg.ShootingRateUpgrade;
            attack.AdditionalDamage += upg.DamageUpgrade;
            attack.AdditionalSpeed += upg.SpeedUpgrade;
            attack.AdditionalForce += upg.ForceUpgrade;
            attack.Projectile = upg.NewProjectile ?? attack.Projectile;
             
        }  
        public void Initialize(Transform handPoint = null)
        {
            if (handPoint != null)
            {
                transform.parent = handPoint;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
            rate = shootRate;
            upgrades.Initialize(); 
            effects.Initialize();
            ammo.Initialize();
        } 
        public void SaveWeaponObject()
        { 
            var saveWeaponObject = new SaveWeaponObject
            {
                name = type.ToString(),
                upgradesLevel = upgrades.UpgradesLevel,
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
                upgrades.UpgradesLevel = loadWeaponObject.upgradesLevel;
                ui.InventorySlot = loadWeaponObject.slot;
                ApplyAllUpgradeToWeapon();
            }
            else
                Debug.LogWarning($"<color=red>No weapon has been loaded for {name} </color>");
        } 
        private void ApplyAllUpgradeToWeapon()
        {
            foreach (UpgradeType ut in Enum.GetValues(typeof(UpgradeType)))
                for (int i = 0; i <= upgrades.UpgradesLevel[ut]; i++)
                    DoUpgrade(upgrades.GetUpgrade(i, ut));
        }
        public virtual void PerformSpecial(PlayerSpecial special)
        {
            if (special.CurrentSpecial >= specialCost && specialRate >= specialTime)
            {
                animator.SetTrigger("Special");
                special.CurrentSpecial -= specialCost;
                specialRate = 0;
            }
        }
        public virtual void DoSpecial() { }
        public void Shake() => effects.Shake(); 
        public void Move() => effects.Move();
    }

}
