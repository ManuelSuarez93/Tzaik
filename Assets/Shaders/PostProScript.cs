using System.Collections;
using Tzaik.General;
using UnityEngine;
using UnityEngine.Serialization;

public class PostProScript : MonoBehaviour
{
    [SerializeField] Material postProcessMaterial;
	[SerializeField] float waveSpeed;
	[SerializeField] bool waveActive;
	float waveDistance;
	 
    public float WaveSpeed { get => waveSpeed; set => waveSpeed = value; }
    public bool WaveActive { get => waveActive; set => waveActive = value; }
    public Material PostProcessMaterial { get => postProcessMaterial; set => postProcessMaterial = value; }
    public float WaveDistance { get => waveDistance; set => waveDistance = value; }

    private void Start()
	{
		Camera cam = GetComponent<Camera>();
		cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
	}

    public void MacahuitlSpecial()
    {
		if (waveActive)
			waveDistance = waveDistance + waveSpeed * Time.deltaTime;
		else
			waveDistance = 0;

    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, postProcessMaterial);
	}
}
 