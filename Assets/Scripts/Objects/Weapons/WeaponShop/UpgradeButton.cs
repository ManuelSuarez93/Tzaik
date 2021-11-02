using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Tzaik.Items.Weapons
{
    public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] bool enabled = true;
        [SerializeField] UnityEvent enterEvent;
        [SerializeField] UnityEvent exitEvent;
        [SerializeField] UnityEvent clickEvent;
        [SerializeField] UnityEvent upEvent;

        Animator animator;
        public bool Enabled { get => enabled; set=> enabled = value;} 
        public UnityEvent ClickEvent { get => clickEvent; set => clickEvent = value; }
        private void Start() => animator = GetComponent<Animator>();
        public void OnPointerExit(PointerEventData eventData)
        {
            if (enabled)
                exitEvent.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(enabled)
                enterEvent.Invoke();
        } 
        public void OnPointerDown(PointerEventData eventData)
        {
            if (enabled)
                clickEvent.Invoke();
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (enabled)
                upEvent.Invoke();
        }

        public void SelectedButton(bool selected) => animator.SetBool("Selected", selected);
        public void ClickedButton(bool clicked) => animator.SetBool("Clicked", clicked);

        
    }
}
