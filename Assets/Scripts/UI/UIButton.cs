 using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Tzaik
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] UnityEvent enterEvent; 
        [SerializeField] UnityEvent exitEvent;

        public void OnPointerEnter(PointerEventData eventData) 
            => enterEvent.Invoke();
        

        public void OnPointerExit(PointerEventData eventData) 
            => exitEvent.Invoke(); 
         
    }
}
