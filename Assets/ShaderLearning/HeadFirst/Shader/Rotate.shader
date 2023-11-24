// Fork from https://www.shadertoy.com/view/ctGyWK
Shader "DarcyStudio/Rotate"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            // float4 _Time;

            // float2x2 R(float a)
            // {
            //     float c = cos(a / 4.0 + 0.0);
            //     float s = sin(a / 4.0 + 0.0);
            //     return float2x2(c, -s, s, c);
            // }

            float2x2 R(float a)
            {
                float4 offset = float4(0, 11, 33, 0);
                float4 cosValues = cos(a / 4.0 + offset);

                return float2x2(cosValues.x, cosValues.y, cosValues.z, cosValues.w);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                float2 size = float2(300, 300);
                o.uv = v.uv * size.xy;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 h = 0;
                h += 1;
                fixed4 O = h;

                float2 u, r = float2(300, 300);
                float A, l, L, a;
                float i_time = 7.0;
                // fixed4 h = 0, O = 0;

                for (; i_time > 0.0; i_time -= 1.0)
                {
                    a -= sin(a -= sin(a = _Time.y * 4.0 + i_time * 0.4));
                    u = (i.uv + i.uv - r) / r.y / 0.1;
                    L = l = max(length(u -= mul(R(a), clamp(mul(u, R(a)), -i_time, i_time))), 1.0);
                    O = lerp(h = sin(i_time + a / 3.0 + float4(1, 3, 5, 0)) * .2 + .7, O,
                             A = min(--l * r.y * .02, 1.0)) *
                        (l + h + .5 * A * u.y / L) / L;
                }

                return O;
            }
            ENDCG
        }
    }
}