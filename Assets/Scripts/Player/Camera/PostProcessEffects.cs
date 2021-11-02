using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Tzaik.Player.Cameras
{
    [ExecuteInEditMode]
    public class PostProcessEffects : MonoBehaviour
    {
        [SerializeField] PostProcessVolume volume;
        [SerializeField] float time; 
        [SerializeField] [Range(0, 1)] float transparency;
        [SerializeField] Image damageImg;
        public Material material;
        public int pixelDensity = 64;
        Vignette vignette;
        private void Start()
        {
            volume.profile.TryGetSettings<Vignette>(out vignette);
        }
        public void SetDamageVignette(float healthPercentage)
        {
            StopAllCoroutines();
            damageImg.color = Color.red; 
            StartCoroutine(setTimerVignette());
        } 
        public void SetHealthVignette(float healthPercentage)
        {
            StopAllCoroutines();
            damageImg.color = Color.blue; 
            StartCoroutine(setTimerVignette());
        }
         
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Vector2 aspectRatioData;
            if (Screen.height > Screen.width)
                aspectRatioData = new Vector2((float)Screen.width / Screen.height, 1);
            else
                aspectRatioData = new Vector2(1, (float)Screen.height / Screen.width);

            material.SetVector("_AspectRatioMultiplier", aspectRatioData);
            material.SetInt("_PixelDensity", pixelDensity);
            Graphics.Blit(source, destination, material);
        }
        IEnumerator setTimerVignette()
        {
            vignette.active = true;
            var timer = 0f;
            var value = transparency;
            while (timer < time)
            {
                value -= Time.deltaTime;
                damageImg.color = new Color(damageImg.color.r, damageImg.color.g, damageImg.color.b, Mathf.Lerp(0,value, timer/time));
                timer += Time.deltaTime;
                yield return null;
            }
            damageImg.color = new Color(damageImg.color.r, damageImg.color.g, damageImg.color.b,0);
        }
    }
     
}
