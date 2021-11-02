Shader "Custom/ToonShaderFire"
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
		_SpecularFalloff("Specular Falloff", Range(0,2)) = 1

		[Header(Dither Parameters)]
		[KeywordEnum(Off, On)]  ENABLEDITHER("Enable Dithering", Float) = 0
		_DitherPattern("Dithering Pattern", 2D) = "white" {}
		_DitherColor("Dither Color", Color) = (1,1,1,1)		 


		[Header(Fresnel)]
		[KeywordEnum(Off, On)]  ENABLEFRESNEL("Enable Fresnel", Float) = 0
		_FresnelColor("Fresnel Color", Color) = (1,1,1,1)
		[PowerSlider(4)] _FresnelExponent("Fresnel Exponent", Range(0.25, 4)) = 1  

		[Header(LiquidEffect)]
		_Amplitude("Wave Size", Range(-1,1)) = 0.4	   
		_Frequency("Wave Frequency", Range(-10, 10)) = 2
		_AnimationSpeed("Animation Speed", Range(0, 5)) = 1
		_CellSize("Cell Size", Range(0,10)) = 1
		_Roughness("Roughness", Range(1,8)) = 3
		_Persistance("Persistance", Range(0,1)) = 0.4 
		_ScrollSpeed("Scroll Speed", Range(0,1)) = 1
	}

		SubShader
		{
			Tags{ "RenderType" = "Transparent " "Queue" = "Transparent" }

			CGPROGRAM

			#include "CustomFunc.cginc"
			#include "UnityCG.cginc"

			#pragma surface surf Stepped fullforwardshadows vertex:vert	addshadow
			#pragma target 3.0
			#pragma multi_compile ENABLEFRESNEL_OFF     ENABLEFRESNEL_ON
			#pragma multi_compile ENABLEDITHER_OFF     ENABLEDITHER_ON

			sampler2D _MainTex;
			fixed4 _Color;
			half3 _Emission;
			fixed4 _Specular;
			sampler2D _Normal;

			#define OCTAVES 4  
			float _Frequency;
			float _AnimationSpeed; 
			float _CellSize;
			float _Roughness;
			float _Persistance;
			float _Amplitude;

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
				color.rgb = s.Albedo * calculateToonLight(s.Normal, lightDir, viewDir, shadowAttenuation, _StepWidth, _StepAmount) * _LightColor0.rgb;
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
				float4 screenPos;
				INTERNAL_DATA
			};
				 
			void vert(inout appdata_full data)
			{
				float3 localPos = data.vertex / data.vertex.w;

				float3 modifiedPos = localPos;
				float2 basePosValue = mul(unity_ObjectToWorld, modifiedPos).xz / _CellSize;
				float basePosNoise = layeredNoise2d(basePosValue, OCTAVES, _Persistance, _Roughness) + 0.5;
				modifiedPos.y += basePosNoise * _Frequency + _Time.y * _AnimationSpeed * _Amplitude;

				//calculate new position based on pos + tangent
				float3 posPlusTangent = localPos + data.tangent * 0.02;
				float2 tangentPosValue = mul(unity_ObjectToWorld, posPlusTangent).xz / _CellSize;
				float tangentPosNoise = layeredNoise2d(tangentPosValue, OCTAVES, _Persistance, _Roughness) + 0.5;
				posPlusTangent.y += tangentPosNoise  *_Frequency +_Time.y * _AnimationSpeed * _Amplitude;

				//calculate new position based on pos + bitangent
				float3 bitangent = cross(data.normal, data.tangent);
				float3 posPlusBitangent = localPos + bitangent * 0.02;
				float2 bitangentPosValue = mul(unity_ObjectToWorld, posPlusBitangent).xz / _CellSize;
				float bitangentPosNoise = layeredNoise2d(bitangentPosValue, OCTAVES, _Persistance, _Roughness) + 0.5;
				posPlusBitangent.y += bitangentPosNoise * _Frequency + _Time.y * _AnimationSpeed * _Amplitude;

				//get recalculated tangent and bitangent
				float3 modifiedTangent = posPlusTangent - modifiedPos;
				float3 modifiedBitangent = posPlusBitangent - modifiedPos;

				//calculate new normal and set position + normal
				float3 modifiedNormal = cross(modifiedTangent, modifiedBitangent);
				data.normal = normalize(modifiedNormal);
				data.vertex = float4(modifiedPos.xyz, 1);
				data.normal.x += rand1dTo1d((data.normal.z * _Frequency + _Time.y * _AnimationSpeed), 0.01) * _Amplitude;
				data.vertex.x += rand1dTo1d((data.vertex.z * _Frequency + _Time.y * _AnimationSpeed), 0.01) * _Amplitude;
				data.normal.y += rand1dTo1d((data.normal.z * _Frequency + _Time.y * _AnimationSpeed), 0.01) * _Amplitude;
				data.vertex.y += rand1dTo1d((data.vertex.z * _Frequency + _Time.y * _AnimationSpeed), 0.01) * _Amplitude;
			}

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
					_DitherPattern,
					_DitherPattern_TexelSize
					, _ScreenParams);

				o.Albedo = lerp(tex2D(_MainTex, i.uv_MainTex), _DitherColor, (finalColor) / 3);

				float3 shadowColor = col.rgb * _ShadowTint;
				o.Emission = _Emission + shadowColor;


				//ADD FRESNEL
				#ifdef ENABLEFRESNEL_ON
					o.Emission = o.Emission + fresnelColor(i.worldNormal, i.viewDir, _FresnelExponent, rand1dTo3d(_FresnelColor));
				#endif		 
			}

			ENDCG
		   		 
		 
		}
			Fallback "Standard"
}

