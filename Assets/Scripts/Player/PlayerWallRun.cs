using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Player
{
    [System.Serializable]
    public class PlayerWallRun
    {
        [SerializeField] float wallrunTime;
        [SerializeField] UnityEvent wallRunEnterEvent, wallRunExitEvent; 
        public float WallrunTime => wallrunTime;
        public UnityEvent WallRunEnterEvent => wallRunEnterEvent; 
        public UnityEvent WallRunExitEvent => wallRunExitEvent;
    }
}
