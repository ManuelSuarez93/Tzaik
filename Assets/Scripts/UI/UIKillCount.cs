using UnityEngine;
using UnityEngine.UI;
using Tzaik.General;

namespace Tzaik.UI
{
    [System.Serializable]
    public class UIKillCount
    {
        [SerializeField] Text killCount; 
        public void ShowKillCount() => killCount.text = killCount != null ? $"Kills: {GameManager.Instance.TotalEnemiesKilled}" : "Kills: 0";
    }
}
