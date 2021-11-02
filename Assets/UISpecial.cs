using Tzaik.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik
{
    public class UISpecial : MonoBehaviour
    {
        PlayerSpecial special;
        [SerializeField] Image img;

        public PlayerSpecial Special { get => special; set => special = value; }

        private void Update() => ShowSpecial();
        public void ShowSpecial()
        {
            if (special != null) 
                if (img != null)
                    img.fillAmount = special.CurrentSpecial / special.TotalSpecial; 
        }
    }
}
