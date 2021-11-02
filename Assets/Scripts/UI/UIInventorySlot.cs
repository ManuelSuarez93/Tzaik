using UnityEngine.UI;
using UnityEngine;

namespace Tzaik.UI
{
    public class UIInventorySlot : MonoBehaviour
    {
        [SerializeField] Image slotImage;
        [SerializeField] Image selectionImage;
        [SerializeField] Sprite unSelectedImage;
        [SerializeField] Color selectedColor;
        [SerializeField] Color unSelectedColor;
        [SerializeField] Sprite selectedImage;
        [SerializeField] Sprite emptyImage;
        public void SetSlotImage(Sprite img = null) 
            => slotImage.color = img != null ? selectedColor : unSelectedColor; 

        public void Selection(bool isSelected)
        {
            selectionImage.sprite = isSelected ? selectedImage : unSelectedImage;
            selectionImage.color = isSelected ? selectedColor : unSelectedColor;
        }
    }
}
