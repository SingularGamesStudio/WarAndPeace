Shader "Hidden/Metaballs2D"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }


    SubShader
    {
        Tags { "RenderType"="Transparent" "RenderPipeline" = "UniversalPipeline" "Queue" = "Transparent" "IgnoreProjector" = "True"}

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
			int _MetaballCount;
			float3 _MetaballData[1000];
            float4 _MetaballColors[1000];
			float _OutlineSize;
			float4 _InnerColor;
			float4 _OutlineColor;
            int gg = 6;
            int gg2 = 6;
			float _CameraSize;

            float4 frag (v2f_img i) : SV_Target
            {
				float4 tex = tex2D(_MainTex, i.uv);
				float dist = 1.0f;
                float deg;
                float mind = 10000000;
                float4 col;

				for (int m = 0; m < _MetaballCount; ++m)
				{
					float2 metaballPos = _MetaballData[m].xy;

					float distFromMetaball = distance(metaballPos, i.uv * _ScreenParams.xy);
					float radiusSize = _MetaballData[m].z * _ScreenParams.y / _CameraSize;
                    if (distFromMetaball < mind) {
                        mind = distFromMetaball;
                        col = _MetaballColors[m];
                        float2 vect = float2(metaballPos.x - (i.uv * _ScreenParams.xy).x, metaballPos.y - (i.uv * _ScreenParams.xy).y);
                        deg = -vect.y/sqrt(vect.x*vect.x+vect.y*vect.y);
                    }
					dist *= saturate(distFromMetaball / radiusSize);
				}

                col.w = 0.5f;
				float threshold = 0.9f;
                if (dist > threshold)
                    return tex;
                dist = dist / threshold;
                deg = deg - 0.2f;
                float dark = (0.0005f) * (pow(2125.0f, dist) - 1);
                col = float4(saturate(col.x + dark * deg*2), saturate(col.y + dark * deg*2), saturate(col.z + dark * deg*2), 0);
                col.w = 0.3f;
                if (dist > 0.5f) 
                    col.w = pow(11.11111f, dist - 1.0f);
                col = float4(col.w * col.x + (1 - col.w) * tex.x, col.w * col.y + (1 - col.w) * tex.y, col.w * col.z + (1 - col.w) * tex.z, 1);
                return col;
            }
            ENDCG
        }
    }
}
