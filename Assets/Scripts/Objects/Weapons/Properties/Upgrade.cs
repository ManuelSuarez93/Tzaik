using Tzaik.LevelObjects;
using UnityEngine;
using static Tzaik.Items.Misc.CoinItem;

namespace Tzaik.Items.Weapons
{
    [System.Serializable]
    public class Upgrade
    {
        [Header("Upgrades")]
        [SerializeField] UpgradeType upgradeType;
        [SerializeField] int level;
        [SerializeField] int updateId;
        [SerializeField] int maxAmmoUpgrade;
        [SerializeField] float shootingRateUpgrade;
        [SerializeField] int damageUpgrade;
        [SerializeField] float speedUpgrade;
        [SerializeField] float forceUpgrade;
        [SerializeField] Projectile newProjectile;
        [SerializeField] int coinCost;
        [SerializeField] CoinType coinType;

        public Projectile NewProjectile => newProjectile; 
        public int UpdateId  => updateId;
        public UpgradeType UpgradeType => upgradeType;
        public int Level => level;
        public int MaxAmmoUpgrade  => maxAmmoUpgrade; 
        public float ShootingRateUpgrade => shootingRateUpgrade; 
        public int DamageUpgrade => damageUpgrade;
        public float SpeedUpgrade  => speedUpgrade;
        public float ForceUpgrade => forceUpgrade;

        public int CoinCost  => coinCost; 
        public CoinType CoinType { get => coinType; set => coinType = value; }
    }



}
