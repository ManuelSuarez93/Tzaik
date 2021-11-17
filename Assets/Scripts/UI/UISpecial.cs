using Tzaik.Items.Weapons;
using Tzaik.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik
{
    [System.Serializable]
    public class UISpecial
    {
        [SerializeField] Image img;
        [SerializeField] Image chargeImg;

        public void ShowSpecial(PlayerSpecial special)
        {
            if (special != null)
                if (img != null)
                    img.fillAmount = special.CurrentSpecial / special.TotalSpecial;
        }

        public void ShowCharge(Weapon w)
        {
            if (w != null)
            {
                chargeImg.gameObject.SetActive(true);
                if (chargeImg != null)
                    chargeImg.fillAmount = w.SpecialRate; 
            }
            else 
                chargeImg.gameObject.SetActive(false); 
        }
    }
}
