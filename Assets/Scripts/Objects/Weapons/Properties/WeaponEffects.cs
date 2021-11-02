using Tzaik.General;
using UnityEngine;
using Tzaik.Player.Cameras;
using System;

namespace Tzaik.Items.Weapons
{
    [Serializable]
    public class WeaponEffects
    {
        [Header("Weapon Sway")]
        [SerializeField] float swayIntenisty;
        [SerializeField] float swaySmoothing;

        [Header("Weapon shake on shot")]
        [SerializeField] Vector3 shakeAmount;
        [SerializeField] float shakeTime;
        [SerializeField] float shakeIntensity;

        [Header("On shoot weapon moving")]
        [SerializeField] Vector3 movement;
        [SerializeField] float moveIntensity;
        [SerializeField] float moveTime;

        WeaponMovingEffects weaponMovingEffects;
        CameraMoveEffect cameraMoveEffect;
        public void Initialize()
        {
            weaponMovingEffects = GameManager.Instance.Player.GetComponentInChildren<WeaponMovingEffects>();
            cameraMoveEffect = GameManager.Instance.Player.GetComponentInChildren<CameraMoveEffect>();
        }
        
        #region Call to other functions
        public void Shake() => cameraMoveEffect.ShakeCamera(shakeAmount, shakeTime, shakeIntensity);
        public void Move() => cameraMoveEffect.MoveCamera(movement, moveIntensity, moveTime);
        public void WeaponSway() => weaponMovingEffects.WeaponSway(swayIntenisty, swaySmoothing);

        #endregion
    }
}
