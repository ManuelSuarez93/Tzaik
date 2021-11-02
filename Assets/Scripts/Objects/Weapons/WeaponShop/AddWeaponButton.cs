using System.Collections;
using System.Collections.Generic;
using Tzaik.Items.Weapons;
using UnityEngine;

namespace Tzaik
{
    public class AddWeaponButton : UpgradeButton 
    {
        int currentSlot;
        public void SetAddSlot(int slot) => currentSlot = slot;
    }
}
