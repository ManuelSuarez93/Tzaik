using System.Collections.Generic;
using Tzaik.LevelObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik.Items.Weapons
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] UpgradeType upgradeType;
        [SerializeField] List<Renderer> images;
        [SerializeField] UpgradeButton upgradeBtn;
        [SerializeField] Color notUpgradedColor;
        [SerializeField] Color upgradedColor;
        int currentLevel = 0;

        public UpgradeType UpgradeType => upgradeType; 
        public UpgradeButton UpgradeBtn { get => upgradeBtn; set => upgradeBtn = value; }
        public void Initialize()
        {
            foreach (Renderer i in images)
                i.material.color = notUpgradedColor;
        }
        public void SetLevelUI(int Level, bool reachedMaxLevel)
        {
            currentLevel = Level;
            if (currentLevel <= images.Count && !reachedMaxLevel && upgradeBtn.Enabled)
                ChangeColorToLevel(currentLevel);

        }

        public void ChangeColorToLevel(int level)
        {
            for (int i = 0; i <= level; i++)
                images[i].material.color = upgradedColor;
        }

        public void ResetButtons()
        {
            Initialize();
            SetInteractable(true);
        }
        public void SetInteractable(bool set)
            => upgradeBtn.Enabled = !set; 
    }
}
