// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "DarcyStudio/Start"
{
    Properties
    {
        _Color ("Color Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Color;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR0;
            };

            v2f vert(const a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.normal * 0.2 + fixed3(0.5, 0.5, 0.5) + 0.3 * v.vertex.xyz;
                return o;
            }

            fixed4 frag(const v2f i) : SV_Target
            {
                fixed3 c = i.color;
                c *= _Color.rgb;
                return fixed4(c, 1.0);
            }
            ENDCG
        }
    }
}