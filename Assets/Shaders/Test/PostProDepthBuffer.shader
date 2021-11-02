Shader "PostProDepthBuffering"
{
	//show values to edit in inspector
	Properties
	{
		[HideInInspector] _MainTex("Texture", 2D) = "white" {}
		[Header(Wave)]
		_WaveDistance ("Distance from player", float) = 10
		_WaveTrail ("Length of the trail", Range(0,5)) = 1
		_WaveColor ("Color", Color) = (1,0,0,1)
	}

		SubShader
		{
		// markers that specify that we don't need culling 
		// or reading/writing to the depth buffer
		Cull Off
		ZWrite Off
		ZTest Always

		Pass
			{
			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			float _WaveDistance;
			float _WaveTrail;
			float4 _WaveColor;

			struct appdata 
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v) 
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET
			{
				//Depth de la camara
				float depth = tex2D(_CameraDepthTexture, i.uv).r;	   
				depth = Linear01Depth(depth);
				depth = depth * _ProjectionParams.z;	  
				

				fixed4 source = tex2D(_MainTex, i.uv);

				float waveFront = step(depth, _WaveDistance);
				float waveTrail = smoothstep(_WaveDistance - _WaveTrail, _WaveDistance, depth);
				float wave = waveFront * waveTrail;   
				if (depth >= _ProjectionParams.z)
					return waveFront;

				fixed4 col = lerp(source, _WaveColor, wave);
				return col;
			}
		ENDCG
		}
	}
}