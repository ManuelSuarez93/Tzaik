using System;
using System.Collections;
using System.Collections.Generic;
using Tzaik.General;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] HealthScript health;
        [SerializeField] Image img;

        private void Update()
        {
            ShowHealth();
            CanBeDamaged();
        }

        void CanBeDamaged()
        {
            if (health != null && img != null)
                if (health.CanBeDamaged)
                    img.color = Color.Lerp(Color.blue, Color.red, Time.deltaTime);
                else
                    img.color = Color.Lerp(Color.red, Color.blue, Time.deltaTime);
                
        }

        void ShowHealth()
        {
            if (health != null && img != null) 
                    img.fillAmount = health.CurrentHealth / health.MaxHealth;
        }


    }
}
