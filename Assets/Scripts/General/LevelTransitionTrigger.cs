using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

namespace Tzaik.General
{
    [RequireComponent(typeof(Collider))]
    public class LevelTransitionTrigger : MonoBehaviour
    {
        #region Fields
        [SerializeField] LayerMask playerLayer;
        [SerializeField] UnityEvent levelTransitionEvent;
        #endregion

        #region Methods
        private void OnTriggerEnter(Collider other)
        {
            if(playerLayer == (playerLayer | (1 << other.gameObject.layer)))
                levelTransitionEvent.Invoke();
        }
        #endregion

    }


}
