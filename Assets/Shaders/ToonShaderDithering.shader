Shader "Custom/ToonShaderOutline"
{
	Properties
	{
		_Color("Color", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		_Specular("Specular Color", Color) = (1,1,1,1)
		_Normal("Normal Map", 2D) = "bump" {}
		[HDR] _Emission("Emission", color) = (0,0,0)	 

		[Header(Lighting Parameters)]
		_ShadowTint("Shadow Color", Color) = (0.5, 0.5, 0.5, 1)
		[IntRange]_StepAmount("Shadow Steps", Range(1, 16)) = 2
		_StepWidth("Step Size", Range(0.05, 1)) = 0.25
		
		_SpecularSize("Specular Size", Range(0,1)) = 0.1
		_SpecularFalloff("Specular Falloff", Range (0,2)) = 1

		[Header(Dither Parameters)]
		[KeywordEnum(Off, On)]  ENABLEDITHER("Enable Dithering", Float) = 0
		_DitherPattern("Dithering Pattern", 2D) = "white" {}
		_DitherColor("Dither Color", Color) = (1,1,1,1)

		[Header(Outline Parameters)]   
		[KeywordEnum(Off, On)] ENABLEOUTLINE("Enable Outline", Float) = 0  
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineThickness("Outline Thickness", Range(0,1)) = 0.03

		
		[Header(Fresnel)]
		[KeywordEnum(Off, On)]  ENABLEFRESNEL("Enable Fresnel", Float) = 0
		_FresnelColor("Fresnel Color", Color) = (1,1,1,1)
		[PowerSlider(4)] _FresnelExponent("Fresnel Exponent", Range(0.25, 4)) = 1
	}

		SubShader
		{
			Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }

			CGPROGRAM
			#include "../Shaders/CustomFunc.cginc"
			#include "UnityCG.cginc"  
			#pragma surface surf Stepped fullforwardshadows	
			#pragma target 3.0
			#pragma multi_compile ENABLEFRESNEL_OFF     ENABLEFRESNEL_ON
			#pragma multi_compile ENABLEDITHER_OFF     ENABLEDITHER_ON

			sampler2D _MainTex;
			fixed4 _Color;
			half3 _Emission;
			fixed4 _Specular;
			sampler2D _Normal;

			
			sampler2D _DitherPattern;
			float4 _DitherPattern_TexelSize;
			float4 _DitherColor;

			float3 _FresnelColor;
			float _FresnelExponent;

			float3 _ShadowTint;
			float _StepAmount;
			float _StepWidth;
			float _SpecularSize;
			float _SpecularFalloff;


			struct ToonSurfaceOutput
			{
				fixed3 Albedo;
				half3 Emission;
				fixed3 Specular;
				fixed Alpha;
				fixed3 Normal;
				float4 screenPosition : TEXCOORD1;
			};

			float4 LightingStepped(ToonSurfaceOutput s, float3 lightDir, half3 viewDir, float shadowAttenuation)
			{	    
				float4 color;
				color.rgb = s.Albedo * calculateToonLight(s.Normal,lightDir, viewDir, shadowAttenuation, _StepWidth, _StepAmount) * _LightColor0.rgb;
				color.rgb = lerp(color.rgb, s.Specular * _LightColor0.rgb, saturate(calculateSpecular(s.Normal, lightDir, viewDir, shadowAttenuation, _SpecularFalloff, _SpecularSize)));
				color.a = s.Alpha;
				return color;
			}

			struct Input
			{
				float2 uv_MainTex;
				float2 uv_Normal;
				float3 worldNormal;
				float3 viewDir;
				float3 worldPos;
				float4 screenPos;
				INTERNAL_DATA
			}; 	   	

			void surf(Input i, inout ToonSurfaceOutput o)
			{						 
				fixed4 col = tex2D(_MainTex, i.uv_MainTex);
				col *= _Color;		 	  
				o.Specular = _Specular;	
				o.Normal = UnpackNormal(tex2D(_Normal, i.uv_Normal)); 
				float3 finalColor = dither(tex2D(_MainTex, i.uv_MainTex).r, 
					tex2D(_MainTex, i.uv_MainTex).g,
					tex2D(_MainTex, i.uv_MainTex).b, 
					i.screenPos.xy / i.screenPos.w, 
					_DitherPattern, _ScreenParams,
					_DitherPattern_TexelSize);

				o.Albedo = lerp(tex2D(_MainTex, i.uv_MainTex), _DitherColor, (finalColor) / 3);
				
				float3 shadowColor = col.rgb * _ShadowTint;
				o.Emission = _Emission + shadowColor;

				
				//ADD FRESNEL
				#ifdef ENABLEFRESNEL_ON
					o.Emission = o.Emission + fresnelColor(i.worldNormal, i.viewDir, _FresnelExponent, rand1dTo3d(_FresnelColor));
				#endif		 
			}

			ENDCG		   
			Pass
			{

				Cull front
				CGPROGRAM

				#include "UnityCG.cginc"
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile ENABLEOUTLINE_OFF     ENABLEOUTLINE_ON

				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed4 _Color;
				fixed4 _OutlineColor;
				float _OutlineThickness;

				struct appdata
				{
					float4 vertex : POSITION;
					float3 normal: NORMAL;
				};

				struct v2f
				{
					float4 position : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;									  
					#ifdef ENABLEOUTLINE_OFF
						o.position = UnityObjectToClipPos(v.vertex);
					#else
						o.position = UnityObjectToClipPos(v.vertex + normalize(v.normal) * _OutlineThickness);
					#endif
						
					return o;
				}

				fixed4 frag(v2f i) : SV_TARGET
				{
					return _OutlineColor;
				}



				ENDCG
			}   
		}
			Fallback "Standard"
}

