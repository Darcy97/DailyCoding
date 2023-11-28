// Fork from https://www.shadertoy.com/view/mtyGWy
Shader "DarcyStudio/Shader0"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _MainTex_ST;
            float2 _Resolution;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed3 palette(float t)
            {
                fixed3 a = fixed3(0.5, 0.5, 0.5);
                fixed3 b = fixed3(0.5, 0.5, 0.5);
                fixed3 c = fixed3(1.0, 1.0, 1.0);
                fixed3 d = fixed3(0.263, 0.416, 0.557);

                return a + b * cos(6.28318 * (c * t + d));
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = (i.uv * 2.0 - 1) / 1;
                float2 uv0 = uv;
                float3 finalColor = float3(0.0, 0.0, 0.0);

                for (float a = 0.0; a < 4.0; a += 1.0)
                {
                    uv = frac(uv * 1.5) - 0.5;
                    float d = length(uv) * exp(-length(uv0));
                    float3 col = palette(length(uv0) + a * 0.4 + _Time.y);
                    d = sin(d * 8.0 + _Time) / 8.0;
                    d = abs(d);
                    d = pow(0.01 / d, 1.2);
                    finalColor += col * d;
                }

                return fixed4(finalColor, 1.0);
            }
            ENDCG
        }
    }
}