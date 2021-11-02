using System.Collections;
using UnityEngine; 

namespace Tzaik.Player.Cameras
{
    public class CameraMoveEffect : MonoBehaviour
    {
        #region Fields
        [Header("Camera shake")]
        //[SerializeField] float shakeTime;
        //[SerializeField] float shakeAmount;
        [SerializeField] Transform pivot, cam;

        Vector3 originalPosition;
        PlayerInventory playerInventory;
        float currentTimer;
        #endregion

        #region Unity Methods
        private void Start() => originalPosition = transform.localPosition;
        #endregion
        #region Methods
        public void ShakeCamera(Vector3 amonut, float shakeTime, float shakeAmount)
        {
            StopAllCoroutines();
            StartCoroutine(Shake(amonut, shakeTime, shakeAmount));
        }
        public void MoveCamera(Vector3 amount, float intensity, float time)
        {
            StopAllCoroutines();
            StartCoroutine(Move(amount, intensity, time));
        }
 
        IEnumerator Shake(Vector3 amount, float shakeTime, float shakeAmount)
        {
            currentTimer = shakeTime; 
            while(currentTimer > 0)
            {
                var rand =  Random.insideUnitSphere;
                pivot.localPosition = 
                    new Vector3(amount.x * rand.x ,
                    amount.y * rand.y ,
                    amount.z * rand.z ) * shakeAmount;
                 
                currentTimer -= Time.deltaTime;
                yield return null;
            }
            pivot.localPosition = originalPosition;
        }
        IEnumerator Move(Vector3 amount, float intensity, float time)
        {
            currentTimer = time;
            while (currentTimer > 0)
            { 
                pivot.localPosition = Vector3.Lerp(pivot.localPosition, amount, Time.deltaTime * intensity);
                currentTimer -= Time.deltaTime;
                yield return null;
            }
            pivot.localPosition = originalPosition;
        }
        #endregion
    }
}
