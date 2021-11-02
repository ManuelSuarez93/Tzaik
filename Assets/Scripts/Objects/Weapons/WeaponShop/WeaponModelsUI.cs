using System; 
using System.Collections.Generic;
using Tzaik.Items.Weapons;
using UnityEngine;

namespace Tzaik.Items.Weapons
{
    public class WeaponModelsUI : MonoBehaviour
    {
        [SerializeField] WeaponType type;
        [SerializeField] GameObject Macahuitl;
        [SerializeField] GameObject Atlatl;
        [SerializeField] GameObject Dagger;
        [SerializeField] GameObject Boleadora;
        [SerializeField] GameObject Blowgun;
        [SerializeField] GameObject Sling;

        Dictionary<WeaponType, GameObject> modelList;
        private void Start() => 
            modelList = new Dictionary<WeaponType, GameObject>
            {
                {WeaponType.Atlatl, Atlatl },
                {WeaponType.Blowgun, Blowgun },
                {WeaponType.Boleadora, Boleadora },
                {WeaponType.Dagger, Dagger },
                {WeaponType.Macahuitl, Macahuitl },
                {WeaponType.Sling, Sling },
            };
        public void SetWeaponModel(WeaponType selectedType)
        {
            foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
                modelList[type].SetActive(type == selectedType);
        }
         

    }
}
