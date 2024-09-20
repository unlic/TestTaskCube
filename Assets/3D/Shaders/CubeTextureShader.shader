Shader "CustomRenderTexture/CubeTextureShader"
{
    Properties
    {
        _FrontTex ("Front Texture", 2D) = "white" {}
        _BackTex ("Back Texture", 2D) = "white" {}
        _LeftTex ("Left Texture", 2D) = "white" {}
        _RightTex ("Right Texture", 2D) = "white" {}
        _TopTex ("Top Texture", 2D) = "white" {}
        _BottomTex ("Bottom Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _FrontTex;
            sampler2D _BackTex;
            sampler2D _LeftTex;
            sampler2D _RightTex;
            sampler2D _TopTex;
            sampler2D _BottomTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Ќормализуем мировую нормаль
                float3 normal = normalize(i.worldNormal);

                fixed4 color = fixed4(1, 1, 1, 1); // ÷вет по умолчанию

                // ¬ыбор текстуры дл€ каждой стороны на основе мировых нормалей
                if (abs(normal.z) > abs(normal.x) && abs(normal.z) > abs(normal.y))
                {
                    if (normal.z > 0.0) // Front
                    {
                        color = tex2D(_FrontTex, i.uv);
                    }
                    else // Back
                    {
                        color = tex2D(_BackTex, i.uv);
                    }
                }
                else if (abs(normal.x) > abs(normal.y))
                {
                    if (normal.x > 0.0) // Right
                    {
                        color = tex2D(_RightTex, i.uv);
                    }
                    else // Left
                    {
                        color = tex2D(_LeftTex, i.uv);
                    }
                }
                else
                {
                    if (normal.y > 0.0) // Top
                    {
                        color = tex2D(_TopTex, i.uv);
                    }
                    else // Bottom
                    {
                        color = tex2D(_BottomTex, i.uv);
                    }
                }

                return color;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
