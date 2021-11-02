using UnityEngine;
using System.Collections.Generic;
using Tzaik.LevelObjects;

namespace Tzaik.Items.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponObject", order = 3)]
    public class WeaponObject : ScriptableObject
    {
        public Weapon weapon;
        [SerializeField] Upgrade[] upgrades;

        public Upgrade[] Upgrades => upgrades;
    }

}
