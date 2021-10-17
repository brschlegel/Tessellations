Shader "Unlit/VoronoiShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

          
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            struct pt
            {
                float3 position;
                float3 color;
            };

            uniform StructuredBuffer<pt> buffer : register(t1);

            uniform int bufferSize;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float EuclidDist(float3 a, float3 b)
            {
                return sqrt(pow(b.x - a.x, 2) + pow(b.y - a.y, 2)+pow(b.z - a.z, 2));
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            

            fixed4 frag (v2f i) : SV_Target
            {
            
                float dist = 1000;
                int curr = 3;
                for(int j = 0; j< bufferSize; j++)
                {
                    float temp = EuclidDist(buffer[j].position, float3(i.uv, 0));
                    if( temp< dist)
                    {
                        dist = temp;
                        curr = j;

                    }
                }
                // sample the texture
                fixed4 col = 0;
                // apply fog
                col.rgb = buffer[curr].color;
                
                return col;
            }
            ENDCG
        }
    }
}
