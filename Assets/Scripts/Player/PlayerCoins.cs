using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tzaik.Items.Misc.CoinItem;

namespace Tzaik.Player
{
    public class PlayerCoins : MonoBehaviour
    {
        #region Fields
        Dictionary<CoinType, int> coinsAmount = new Dictionary<CoinType, int>();
        #endregion

        #region Properties
        public Dictionary<CoinType, int> CoinsAmount => coinsAmount;
        #endregion 
        public void InitializeDictionary()
        {
            coinsAmount = new Dictionary<CoinType, int>();
            foreach(CoinType t in Enum.GetValues(typeof(CoinType))) 
                coinsAmount.Add(t, 0); 
            
        }
        public void AddCoin(CoinType type, int amount) => coinsAmount[type] += amount;
        public void RemoveCoin(CoinType type, int amount) => coinsAmount[type] -= amount;
        public void SetCoin(CoinType type, int amount) => coinsAmount[type] = amount;
    }
}
