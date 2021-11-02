Shader "Custom/Noise"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Amplitude("Amplitude", Range(0,10)) = 1
        _AnimationTime("Aniamtion Time", Range(0, 10)) = 1
        _CellSize("Cell Size", Range(0,10)) = 1                   
    }
    SubShader
    {
        Tags { "RenderType"="Transparency" "Queue" = "Transparent"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade 
        #include "CustomFunc.cginc"
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
                         
        sampler2D _MainTex;            
        float _CellSize;     
        float _Amplitude;
        float _MinDistToCell;
        float _AnimationTime;

        struct Input
        {
            float3 worldPos;
            float4 screenPos;
            float2 uv_MainTex;
        };             

       
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
                                            
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {          
            fixed4 col = tex2D(_MainTex, IN.uv_MainTex * - 1) * _Color;
            o.Albedo = voronoiNoiseWithEdges((IN.worldPos/ _CellSize) +(_Time * _AnimationTime), col.rgb);
            o.Albedo *= col;
            o.Alpha = col.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
