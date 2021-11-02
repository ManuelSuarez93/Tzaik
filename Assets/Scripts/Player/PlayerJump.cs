using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Player
{
    [System.Serializable]
    public class PlayerJump
    {
        [SerializeField] float jumpHeight, jumpHeightdouble;
        [SerializeField] int totaljumps = 2;
        [SerializeField] UnityEvent jumpEvent;
        int currentJumps;

        float DoJump(bool doubleJump) => doubleJump ? jumpHeightdouble : jumpHeight;
        public float JumpHeight => jumpHeight;
        public void Jumping(Rigidbody playerRigidbody)
        {  
            if (!ReachedMaxJumps)
             {
                currentJumps++; 
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, DoJump(currentJumps == 2)); 
                jumpEvent.Invoke();
            }
        }

        public void ResetJump() => currentJumps = 0;

        bool ReachedMaxJumps => currentJumps >=totaljumps;

    }
}
