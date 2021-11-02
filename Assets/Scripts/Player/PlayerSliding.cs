using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Player
{
    [System.Serializable]
    public class PlayerSliding
    {
        [SerializeField] float slidingTime;
        [SerializeField] UnityEvent slidingEnterEvent, slidingExitEvent; 
        public float SlidingTime => slidingTime;
        public UnityEvent SlidingEnterEvent => slidingEnterEvent; 
        public UnityEvent SlidingExitEvent => slidingExitEvent;
    }
}
