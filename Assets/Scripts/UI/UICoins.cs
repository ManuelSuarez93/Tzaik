using Tzaik.Player;
using UnityEngine;
using UnityEngine.UI;
using static Tzaik.Items.Misc.CoinItem;

namespace Tzaik.UI
{
    [System.Serializable]
    public class UICoins
    {

        PlayerCoins playerCoins;
        [SerializeField] Text jadeCoinsText;
        [SerializeField] Text idolsText;
        [SerializeField] Text crystalTabletText;
         
 
        public void ShowCoins(PlayerCoins playerCoins)
        {
            if(playerCoins != null)
            {
                jadeCoinsText.text = playerCoins.CoinsAmount[CoinType.JadeCoin].ToString();
                idolsText.text = playerCoins.CoinsAmount[CoinType.GoldenIdol].ToString();
                crystalTabletText.text = playerCoins.CoinsAmount[CoinType.CrystalTablet].ToString();
            }
        }
        
    }
}
