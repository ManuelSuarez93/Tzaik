using UnityEngine;
using System.Collections.Generic;
using Tzaik.LevelObjects;
using System;
using System.Linq;

namespace Tzaik.Items.Weapons
{
    [Serializable]
    public class WeaponUpgrades
    {
        [SerializeField] string upgradeText1;
        [SerializeField] string upgradeText2;
        [SerializeField] string specialUpgradeText;
        [SerializeField] List<Upgrade> upgrades;
        Dictionary<UpgradeType, int> upgradesLevel; 
        Dictionary<UpgradeType, int> maxLevel;
        Dictionary<UpgradeType, List<Upgrade>> appliedUpgrades; 

        public Dictionary<UpgradeType, int> UpgradesLevel { get => upgradesLevel; set => upgradesLevel = value; }
        public string SpecialUpgradeText => specialUpgradeText; 
        public string UpgradeText1  => upgradeText1; 
        public string UpgradeText2  => upgradeText2; 

        public bool ReachedMaxLevel(UpgradeType type) => upgradesLevel[type] >= maxLevel[type]; 
        public void Initialize()
        {
            upgradesLevel = new Dictionary<UpgradeType, int>();
            foreach (UpgradeType t in Enum.GetValues(typeof(UpgradeType)))
                upgradesLevel[t] = -1;

            maxLevel = new Dictionary<UpgradeType, int>();
            foreach (UpgradeType t in Enum.GetValues(typeof(UpgradeType)))
            {
                var level = upgrades.
                    Where(x => x.UpgradeType == t && x != null).
                    OrderByDescending(x => x.Level).FirstOrDefault();

                maxLevel.Add(t, level != null ? level.Level : -1);
            }
        }

        public Upgrade GetUpgrade(int level, UpgradeType upgradeType)
         => upgrades.FirstOrDefault(x => x.UpgradeType == upgradeType && x.Level == level);

    }

}
