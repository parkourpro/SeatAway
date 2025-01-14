Shader "DissolverShader/DissolveShader" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("Normal Map", 2D) = "bump" {}
		_NormalStrenght ("Normal Strength", Range(0, 1.5)) = 0.5
		_DissolveMap ("Dissolve Map", 2D) = "white" {}
		_DissolveAmount ("DissolveAmount", Range(0, 1)) = 0
		_DissolveColor ("DissolveColor", Vector) = (1,1,1,1)
		_DissolveEmission ("DissolveEmission", Range(0, 1)) = 1
		_DissolveWidth ("DissolveWidth", Range(0, 0.1)) = 0.05
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
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
	Fallback "Diffuse"
}