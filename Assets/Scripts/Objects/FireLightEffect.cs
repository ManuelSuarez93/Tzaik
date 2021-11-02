using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tzaik
{ 
    //[ExecuteInEditMode]
    public class FireLightEffect : MonoBehaviour
    {
        [SerializeField] Light light;
        [SerializeField] float positionScrollSpeed = 5f;
        [SerializeField] float scale = 1f;
        Vector3 originalPos;
        private void Start() => originalPos = transform.localPosition;
        void Update()
        {
            light.intensity = (Mathf.PerlinNoise(Time.time * positionScrollSpeed , Time.time * positionScrollSpeed) * scale);
            transform.localPosition = originalPos + (Position() * scale);
        }
        Vector3 Position()
        { 
            float x = (Mathf.PerlinNoise(Time.time * positionScrollSpeed , Time.time * positionScrollSpeed) - 0.5f);
            float y = (Mathf.PerlinNoise(Time.time * positionScrollSpeed , Time.time * positionScrollSpeed) - 0.5f);
            float z = (Mathf.PerlinNoise(Time.time * positionScrollSpeed , Time.time * positionScrollSpeed) - 0.5f);
            return new Vector3(x, y, z);
        }
    }
}
