using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Player
{
    [System.Serializable]
    public class PlayerDashing
    {
        [SerializeField] float dashTime = 0.1f, dashAmount = 5;
        [SerializeField] UnityEvent dashEnterEvent, dashExitEvent;
        [SerializeField] bool isDashing;
        public bool IsDashing { get => isDashing; set => isDashing = value; }
        public float DashTime { get => dashTime; set => dashTime = value; }
        public UnityEvent DashEnterEvent  => dashEnterEvent;  
        public UnityEvent DashExitEvent  => dashExitEvent;  
        public void Dashing(Rigidbody rigid, Vector3 CamRight, bool IsRight) => rigid.velocity = (CamRight * (IsRight ? dashAmount : -dashAmount));
 
    }
}
