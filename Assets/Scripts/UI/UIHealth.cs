using Tzaik.General;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik
{
    [System.Serializable]
    public class UIHealth
    { 
        [SerializeField] Image img;  
        public void ShowHealth(HealthScript health)
        {
            if (health != null) 
                if(img != null)
                    img.fillAmount = health.CurrentHealth / health.MaxHealth; 
        }
    }
}
