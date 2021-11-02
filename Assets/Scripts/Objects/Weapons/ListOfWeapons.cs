using UnityEngine;
using System.Collections.Generic;

namespace Tzaik.Items.Weapons
{
    [CreateAssetMenu(fileName = "ListOfWeapons", menuName = "ScriptableObjects/ListOfWeapons", order = 2)]
    public class ListOfWeapons : ScriptableObject
    {
        public List<Weapon> weapon;
    }

}
