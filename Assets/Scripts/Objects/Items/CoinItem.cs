using Tzaik.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tzaik.General;

namespace Tzaik.Items.Misc
{
    public class CoinItem : TriggerCollisionObject
    {
        [SerializeField] CoinType type;
        [SerializeField] int coinAmount;
        PlayerCoins playerCoins;
        private void Awake() => playerCoins = GameManager.Instance.Player.GetComponent<PlayerCoins>();
        public enum CoinType { JadeCoin, GoldenIdol, CrystalTablet }
        public override void DoAction(GameObject obj) => playerCoins.AddCoin(type, coinAmount);  
    }

}
