Shader "Unlit/LensDistortion"
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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float _LensDistortionTightness = 0.5;
            float _LensDistortionStrength = 0.5;
float4 _OutOfBoundColour = float4(0.5, 0.5, 0.5, 0.5);
            fixed4 frag (v2f i) : SV_Target
            {
                // fixed2 uv_centered = fract(uv) - .5;
                const float2 uvNormalized = i.uv * 2 - 1; //change UV range from (0,1) to (-1,1)
                const float distortionMagnitude = abs(uvNormalized[0] * uvNormalized[1]); //get value with 1 at corner and 0 at middle
                const float smoothDistortionMagnitude = pow(distortionMagnitude, _LensDistortionTightness); //use exponential function
  
                float2 uvDistorted = i.uv + uvNormalized * smoothDistortionMagnitude * _LensDistortionStrength; //vector of distortion and add it to original uv
    return smoothDistortionMagnitude; // previewing smooth distortion map
                    return tex2D(_MainTex, uvDistorted);
              //Handle out of bound uv
                if (uvDistorted[0] < 0 || uvDistorted[0] > 1 || uvDistorted[1] < 0 || uvDistorted[1] > 1)
                {
                    return _OutOfBoundColour; //uv out of bound so display out of bound color
                }
                else
                {
                }
            }
            ENDCG
        }
    }
}
