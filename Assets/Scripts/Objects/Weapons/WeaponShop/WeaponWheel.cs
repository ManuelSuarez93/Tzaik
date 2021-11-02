using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tzaik
{
    public class WeaponWheel : MonoBehaviour
    {
        [SerializeField] float changeSpeed; 
        float time;

        public void DoChangeWeapon(float moveTo)
            => StartCoroutine(ChangeWeapon(moveTo)); 
        IEnumerator ChangeWeapon(float moveTo)
        {
            time = 0;
            var newPos = new Vector3(0, 0, moveTo);
            while (time < changeSpeed)
            {
                time += Time.deltaTime;
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, newPos, time);
                yield return null;
            }
            transform.localEulerAngles = newPos;
        }
    }
}
