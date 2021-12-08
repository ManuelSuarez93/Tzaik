using System.Collections.Generic;
using static Tzaik.Items.Misc.CoinItem;
using Tzaik.Items.Weapons;

namespace Tzaik.SaveSystem
{
    public class SavePlayerObject
    {
        public string name;
        public int enemiesKilled; 
        public List<WeaponType> weapons;
    }
 
}
