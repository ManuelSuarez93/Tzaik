 using Tzaik.General;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik.UI
{
    [System.Serializable]
    public class UICurrentLevel
    {
        [SerializeField] Text currentLevel; 
        public void ShowCurrentLevel() => currentLevel.text = currentLevel != null ? $"Level: {GameManager.Instance.CurrentLevel}" : "Level: 0";
    }
}
