Shader "Hovl/Particles/Blood" {
	Properties {
		_Cutoff ("Mask Clip Value", Float) = 0.5
		_MainTex ("MainTex", 2D) = "white" {}
		_SpeedMainTexUVNoiseZW ("Speed MainTex U/V + Noise Z/W", Vector) = (0.1,0.05,0,0)
		_Color ("Color", Vector) = (1,0,0,1)
		_Normalmaplightning ("Normal map lightning", Float) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		_Gloss ("Gloss", Range(0, 1)) = 0.7
		_NormapMap ("Normap Map", 2D) = "bump" {}
		_Emission ("Emission", Float) = 1
		[HideInInspector] _tex3coord ("", 2D) = "white" {}
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}