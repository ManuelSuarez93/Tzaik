using Tzaik.Items.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik.LevelObjects
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] TriggerCollisionObject interactable;
        Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }
        public void UseItem()
        {
            if (interactable.DoActionReturnBool()) button.interactable = false;
        }
    }
}
