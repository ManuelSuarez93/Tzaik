using UnityEngine;

namespace Tzaik.Player
{ 
    public class PlayerSpecial : MonoBehaviour
    {
        [SerializeField] float totalSpecial;
        
        float currentSpecial;
        public float CurrentSpecial { get => currentSpecial; set => currentSpecial = value; }
        public float TotalSpecial { get => totalSpecial; set => totalSpecial = value; }
        private void Start() => currentSpecial = totalSpecial;
        public void AddSpecial(float special) => currentSpecial += special; 
        public void RemoveSpecial(float special) => currentSpecial -= special;
    }
}