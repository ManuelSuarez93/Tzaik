using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.General
{
    public class TimeEvent : MonoBehaviour
    {
        [SerializeField] float aliveTimeMin;
        [SerializeField] float aliveTimeMax;
        [SerializeField] bool TimerOn = true;
        float curreintTime;
        [SerializeField] UnityEvent onTimedEvent;
        private void OnEnable() => curreintTime = aliveTimeMin;
        private void Update() =>TimedEvent();
        void TimedEvent()
        {
            if(TimerOn)
            {
                if (curreintTime > 0)
                    curreintTime -= Time.deltaTime;
                else
                    onTimedEvent.Invoke();
            }
        }

        public void SetTimer(bool b) => TimerOn = b;
        public void ResetTimer() => curreintTime = aliveTimeMin;
        public void ResetTimerRandom() => curreintTime = Random.Range(aliveTimeMin, aliveTimeMax);
        public void DestroyObject() => Destroy(gameObject);
        
    }
}
