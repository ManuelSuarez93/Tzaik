using Tzaik.General;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik
{
    [System.Serializable]
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] Image img;
        [SerializeField] HealthScript health;
        public HealthScript Health { get => health; set => health = value; }
        void Update()
        {
            ShowHealth();
        }
        public void ShowHealth()
        {
            if (Health != null)
            {
                if(text != null) 
                    text.text = $"{Health.CurrentHealth}/{Health.MaxHealth}";
                if(img != null)
                    img.fillAmount = Health.CurrentHealth / Health.MaxHealth;
            }
        }
    }
}
