using System;
using Tzaik.General;
using Tzaik.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tzaik.Player
{
    [System.Serializable]
    public static class InputManager
    {  
        static PlayerControls playerControls;
        static InputAction movement;
        static PlayerController playerController;
        #region Properties
        public static bool IsLeft { get => movement.ReadValue<Vector2>().x <= -1; }
        public static bool IsRight { get => movement.ReadValue<Vector2>().x >= 1; } 
        public static bool IsJumping { get; private set; }
        public static float Horizontal { get; private set; }
        public static float Vertical { get; private set; }
        public static bool IsPaused { get; private set; }
        public static bool IsSprinting { get; private set; }
        public static bool IsCrouching { get;  set; }
        public static bool IsRightMouseClick { get; private set; }
        public static bool IsRightMouseClickUp { get; private set; }
        public static bool IsDashing { get; private set; } 
        public static bool IsMoving { get; private set; }
        public static Vector2 MousePosition
            => playerControls.Player.Look.ReadValue<Vector2>() * Time.deltaTime; /*playerControls.Player.Look.ReadValue<Vector2>().normalized * Time.deltaTime; */
        public static bool IsUse { get; private set; } 
        public static PlayerControls PlayerControls { get => playerControls; set => playerControls = value; }
        public static InputAction Movement  => movement; 
        public static void Initialize(PlayerController controller)
        { 
            ShowMouse(IsPaused);
            playerControls = new PlayerControls();
            playerController = controller;
            playerControls.Enable();


            playerControls.Player.Jump.Enable();
            playerControls.Player.Jump.started += JumpPerformed;
            playerControls.Player.Jump.canceled += JumpCancelled;

            playerControls.Player.Left.Enable();
            playerControls.Player.Left.performed += LeftPerformed;
            playerControls.Player.Left.canceled += LeftCanceled;
            playerControls.Player.Right.Enable();
            playerControls.Player.Right.performed += RightPerformed; ;
            playerControls.Player.Right.canceled += RightCanceled;

            playerControls.Player.Interact.Enable();
            playerControls.Player.Interact.performed += Interact;

            playerControls.Player.Look.Enable();
            
            playerControls.Player.Dashing.Enable();
            playerControls.Player.Dashing.performed += DashnigPerformed;
            playerControls.Player.Dashing.canceled += DashingCanceled;

            playerControls.Player.Shoot.Enable();
            playerControls.Player.Shoot.performed += PerformAttack;
            playerControls.Player.Shoot.canceled += PerformRelease;

            playerControls.Player.Special.Enable();
            playerControls.Player.Special.performed += PerformSpecial; 

            playerControls.Player.Escape.Enable();
            playerControls.Player.Escape.performed += Escape_performed;
            playerControls.UI.Exit.Enable();
            playerControls.UI.Exit.performed += Escape_performed;

            playerControls.Player.Crouch.Enable();
            playerControls.Player.Crouch.performed += CrouchPerformed;
            playerControls.Player.Crouch.canceled += CrouchCanceled;

            playerControls.Player.Sprint.Enable();
            playerControls.Player.Sprint.performed += SprintPerformed;
            playerControls.Player.Sprint.canceled += SprintCanceled;

            PlayerControls.Player.SwitchWeapon.Enable();
            PlayerControls.Player.SwitchWeapon.performed += SwitchWeaponPerformed;

            PlayerControls.Player.Weapon1.Enable();
            PlayerControls.Player.Weapon1.performed += Weapon1Performed;
            PlayerControls.Player.Weapon2.Enable();
            PlayerControls.Player.Weapon2.performed += Weapon2Performed;
            PlayerControls.Player.Weapon3.Enable();
            PlayerControls.Player.Weapon3.performed += Weapon3Performed;
            movement = playerControls.Player.Movement;
            movement.Enable();
            movement.performed += MovementPerformed;
            movement.canceled += MovementCancelled; 
        }

        private static void RightPerformed(InputAction.CallbackContext obj) => playerController.DoCoroutine("SidewaysRightCoroutine");

        private static void LeftCanceled(InputAction.CallbackContext obj) => playerController.DoCoroutine("CenterCameraFromLeft");

        private static void RightCanceled(InputAction.CallbackContext obj) => playerController.DoCoroutine("CenterCameraFromRight");

        private static void LeftPerformed(InputAction.CallbackContext obj) => playerController.DoCoroutine("SidewaysLeftCoroutine");

        public static void PauseGame(bool showMouse, bool showMenu, bool pause, bool changeInput, bool stopTime)
        {  
            if(stopTime) GameManager.Instance.Pause(pause);
            if(showMouse) ShowMouse(pause);
            if(showMenu) UIManager.Instance.Pause(pause); 
            if(changeInput) InputEnable(pause);
        } 
        static void InputEnable(bool pause)
        {
            if (pause)
            { 
                playerControls.Player.Disable();
                playerControls.UI.Enable();
            }
            else
            { 
                playerControls.Player.Enable(); 
                playerControls.UI.Enable();
            }
        }
        private static void PerformAttack(InputAction.CallbackContext obj)
           => playerController.Inventory.PerformAttack(); 
        private static void PerformRelease(InputAction.CallbackContext obj)
            => playerController.Inventory.PerformRelease();
        private static void PerformSpecial(InputAction.CallbackContext obj) 
            => playerController.Inventory.PerformSpecial(playerController.Special);

        private static void DashingCanceled(InputAction.CallbackContext obj)
            => IsDashing = false;
        private static void DashnigPerformed(InputAction.CallbackContext obj)
            => IsDashing = true;  
        private static void MovementPerformed(InputAction.CallbackContext obj) 
            => IsMoving = true;
        private static void MovementCancelled(InputAction.CallbackContext obj) 
            => IsMoving = false;
        private static void JumpPerformed(InputAction.CallbackContext obj)
        {
            IsJumping = true;
            Debug.Log("JUMP STARTED;");
        }

        private static void JumpCancelled(InputAction.CallbackContext obj)
        {
            IsJumping = false;
            Debug.Log("JUMP CANCELLED;");
        }

        private static void CrouchPerformed(InputAction.CallbackContext obj)
            => IsCrouching = true;
        private static void CrouchCanceled(InputAction.CallbackContext obj)
            => IsCrouching = false;
        private static void Interact(InputAction.CallbackContext obj)
            => playerController.Interact.Interact(playerController.Checks.GetInteractable());

        private static void SwitchWeaponPerformed(InputAction.CallbackContext obj)
            => playerController.Inventory.SelectItem((int)PlayerControls.Player.SwitchWeapon.ReadValue<float>(), true);
        private static void Weapon3Performed(InputAction.CallbackContext obj) 
            => playerController.Inventory.SelectItem(2);

        private static void Weapon2Performed(InputAction.CallbackContext obj)
            => playerController.Inventory.SelectItem(1);

        private static void Weapon1Performed(InputAction.CallbackContext obj)
            => playerController.Inventory.SelectItem(0);
        private static void SprintCanceled(InputAction.CallbackContext obj)
           => IsSprinting = false;
        private static void SprintPerformed(InputAction.CallbackContext obj)
           => IsSprinting = true;

        private static void Escape_performed(InputAction.CallbackContext obj)
        {
            IsPaused = !IsPaused;
            PauseGame(true, true, IsPaused, true, true);
        }
        #endregion 
        public static int GetNumInputs()
        {
            if (Input.GetKey(KeyCode.Alpha0))
                return 0;
            if (Input.GetKey(KeyCode.Alpha1))
                return 1;
            if (Input.GetKey(KeyCode.Alpha2))
                return 2;
            if (Input.GetKey(KeyCode.Alpha3))
                return 3; 
            else return -1;
        }
         
        public static void ShowMouse(bool paused)
        {
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = paused ? true : false;
        }
         
    }
}
