 using UnityEngine;
using Tzaik.General;
using System;

namespace Tzaik.Player.Cameras
{
    [System.Serializable]
    public class PlayerMouseLook 
    {
        #region Fields
        [SerializeField] Transform pivot;
        [SerializeField] float sensitivityXAxis;
        [SerializeField] float sensitivityYAxis;
        [SerializeField] bool invertedX;
        [Header("Sideway rotation settings")]
        [SerializeField] float sidewaysRate;
        [SerializeField] float zRotationMax; 
        [SerializeField] float sidewaysTolerance;
        [Header("Sideways rotation for wallrun")]
        [SerializeField] float wallrunSidewaysRate;
        [SerializeField] float wallrunSidewaysMax;
        [Header("Zoom options")]
        [SerializeField] float zoomAmount; 

        public bool IsWallRun { get; set; }
        public bool IsSliding { get; set; } 
        SidewaysCamera sideways = new SidewaysCamera();
        float xRotation;
        float yRotation;
        float zRotation;
        #endregion

        #region Properties
        public Vector3 GetCamRight => new Vector3(pivot.transform.right.x, 0, pivot.transform.right.z).normalized;
        public Vector3 GetCamForward => new Vector3(pivot.transform.forward.x, 0, pivot.transform.forward.z).normalized;
        public SidewaysCamera Sideways => sideways; 
        public float ZRotationMax => zRotationMax; 
        public float ZRotation { get => zRotation; set => zRotation = value; }
        public float SidewaysTolerance  => sidewaysTolerance; 
        #endregion

        #region Methods
        public void MouseLooking()
        {
            xRotation -= InputManager.MousePosition.y * sensitivityXAxis;
            yRotation += InputManager.MousePosition.x * sensitivityYAxis;
            xRotation = Mathf.Clamp(xRotation, -89.9f, 89.9f); 
            pivot.localEulerAngles = new Vector3(invertedX ? xRotation * -1 : xRotation, yRotation ,zRotation);   
        }
        public void SidewaysRight()
        {
            if (!IsWallRun && !IsSliding)
                sideways.SidewaysRight(ref zRotation, zRotationMax, sidewaysRate); 
        }

        public void SidewaysLeft()
        {
            if (!IsWallRun && !IsSliding)
                sideways.SidewaysLeft(ref zRotation, zRotationMax, sidewaysRate);
        }

        public void CenterCamera() => sideways.CenterCamera(ref zRotation, sidewaysRate);

        public void InputSidewaysLeft() => sideways.SidewaysRight(ref zRotation, zRotationMax, sidewaysRate); 
        public void InputSidewaysRight() => sideways.SidewaysRight(ref zRotation, zRotationMax, sidewaysRate);
          
        public void DoWallRunSideways(bool isRight) 
            =>  sideways.WallRunSideways(ref zRotation, wallrunSidewaysMax, isRight, wallrunSidewaysRate);


        #endregion
    }
}
