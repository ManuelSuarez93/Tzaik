using Tzaik.Player; 
using UnityEngine;

namespace Tzaik.Items.Weapons
{
    public class WeaponMovingEffects : MonoBehaviour
    { 
        Quaternion originRotation; 

        private void Start() => originRotation = transform.localRotation;
        public void WeaponSway(float intensity, float smoothing)
        {
            var y = InputManager.MousePosition.y;
            var x = InputManager.MousePosition.x;

            var adjX = Quaternion.AngleAxis(-intensity * x, Vector3.forward);
            var adjY = Quaternion.AngleAxis(intensity * y, Vector3.right); 
            var targetRotation = originRotation * adjX * adjY; 

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smoothing); 
        }
    }
}
