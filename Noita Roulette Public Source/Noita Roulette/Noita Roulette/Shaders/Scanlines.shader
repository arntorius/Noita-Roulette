Shader "Custom/Scanlines"
{
	Properties
	{
        _Color("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
        _RippleTexA("Ripple Texture A", 2D) = "black" {}
        _RippleTexB("Ripple Texture B", 2D) = "white" {}
        _RipplePeriodA("Ripple Period A", Range(0.1, 20.0)) = 10
        _RipplePeriodB("Ripple Period B", Range(0.1, 20.0)) = 3
        _RippleAmplitude("Ripple Amplitude", Range(0.0, 8.0)) = 1
        _ScanlinePixelHeight("Scanline Pixel Height", Range(2, 32)) = 4
        _UnscaledTime("Unscaled Time", float) = 0
	}
	SubShader
	{
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
        }
		LOD 100
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest On
        Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

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
                float4 uv2 : TEXCOORD1;
			};

            uniform fixed4 _Color;
            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform sampler2D _RippleTexA;
            uniform float4 _RippleTexA_ST;
            uniform sampler2D _RippleTexB;
            uniform float4 _RippleTexB_ST;
            uniform fixed _RipplePeriodA;
            uniform fixed _RipplePeriodB;
            uniform fixed _RippleAmplitude;
            uniform half _ScanlinePixelHeight;
            uniform fixed _UnscaledTime;
            
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.uv2 = ComputeScreenPos(o.vertex);
                o.uv2 /= o.uv2.w;

                return o;
			}
			
			fixed4 frag(v2f i) : SV_Target
			{
                fixed4 color = tex2D(_MainTex, i.uv);
                color *= _Color;

                if (frac(i.uv2.y * _ScreenParams.y * 0.5 / _ScanlinePixelHeight) > 0.5f) { color.rgb *= 0.7f; }

                fixed4 rippleColorA = tex2D(_RippleTexA,
                    fixed2(i.uv2.x, i.uv2.y * 0.25 + frac(_UnscaledTime / _RipplePeriodA))
                );
                fixed4 rippleColorB = tex2D(_RippleTexB,
                    fixed2(i.uv2.x, i.uv2.y * 0.25 - frac(_UnscaledTime / _RipplePeriodB))
                );

                color.rgb += rippleColorA.rgb * rippleColorB.rgb * _RippleAmplitude;
                
                return color;
			}
			ENDCG
		}
	}
}
