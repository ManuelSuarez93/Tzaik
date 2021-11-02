using System.Collections.Generic;
using Tzaik.Items.Weapons; 

namespace Tzaik.SaveSystem
{
    public class SaveWeaponObject
    {
        public string name;
        public Dictionary<UpgradeType, int> upgradesLevel;
        public int slot;
    }
 
}
