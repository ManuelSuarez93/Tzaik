using Tzaik.Audio;
using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Player.Cameras
{
    [System.Serializable]
    public class HeadBobbing
    {
        #region Fields 
        [SerializeField] Transform cam;
        [Header("Up/Down Bobbing")]
        [SerializeField] bool UseY;
        [SerializeField] float maxY;
        [SerializeField] float minY;
        [Header("Left/Right Bobbing")]
        [SerializeField] bool UseX;
        [SerializeField] float maxX;
        [SerializeField] float minX;

        [Header("Speed")]
        [SerializeField] float sprintSpeed;
        [SerializeField] float crouchSpeed;
        [SerializeField] float walkSpeed;

        [Header("Misc")]
        [SerializeField] float crouchAmount;
        [SerializeField] UnityEvent onStep;

        float currentRate;
        float currentX;
        float currentY;
        bool right;
        bool up;
        #endregion

        #region Properties
        public float CurrentRate { get => currentRate; set => currentRate = value; }
        public bool Crouching { get; set; }
        public float SprintSpeed => sprintSpeed;  
        public float CrouchSpeed  => crouchSpeed;
        public float WalkSpeed => walkSpeed;
        #endregion

        #region Methods
        private void Start()
        {
            currentY = maxY;
            currentRate = walkSpeed;
            currentX = 0;
        }
        public void DoHeadBobbing()
        {
            if(UseX)
                SetX();
            if(UseY)
                SetY();
            cam.localPosition = new Vector3(currentX, Crouching ? currentY / crouchAmount : currentY);
        }
        void SetX()
        {
            if (right)
            {
                if (currentX <= maxX)
                    currentX += Time.deltaTime * currentRate; 
                else
                {
                    right = false; 
                }
            }
            else
            {
                if (currentX >= minX)
                    currentX -= Time.deltaTime * currentRate;
                else
                {
                    right = true;
                }
            }
        }
        void SetY()
        {
            if (up)
            {
                if (currentY <= maxY)
                    currentY += Time.deltaTime * currentRate;
                else
                { 
                    up = false;
                    onStep.Invoke();
                }
            }
            else
            {
                if (currentY >= minY)
                    currentY -= Time.deltaTime * currentRate;
                else
                { 
                    up = true;
                    onStep.Invoke();
                }
            }
        }
        #endregion

    }
}
