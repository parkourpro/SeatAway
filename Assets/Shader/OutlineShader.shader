Shader "Custom/OutlineShader"
{
    Properties
    {
        _Color ("Outline Color", Color) = (1,1,1,1)
        _Thickness ("Outline Thickness", Range(0, 0.1)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Cull Front
            ZWrite On
            ColorMask 0
            Blend SrcAlpha OneMinusSrcAlpha
            Offset 1, 1

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR;
            };

            fixed4 _Color;
            float _Thickness;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.pos.xy += o.pos.w * _Thickness * float2(1, 1); // Tăng độ dày viền
                o.color = _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return i.color;
            }
            ENDHLSL
        }
    }
}