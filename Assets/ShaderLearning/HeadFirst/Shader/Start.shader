// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "DarcyStudio/Start"
{
    Properties
    {
        _Color ("Color Tint", Color) = (1,1,1,1)
    }
    SubShader
    {

        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent"
        }

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

                float2 size = float2(300, 300);
                float x = v.texcoord * size.x - size.x/2;

                o.color = fixed3(1.0, 1.0, x / 150);
                return o;
            }

            fixed4 frag(const v2f i) : SV_Target
            {
                fixed3 c = i.color;
                // c *= _Color.rgb;
                return fixed4(c, 1.0);
            }
            ENDCG
        }
    }
}