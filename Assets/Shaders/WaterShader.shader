Shader "Shaders/WaterShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_WaveTex("Wave Texture", 2D) = "white" {}
		_DisplaceTex("Displacement Texture", 2D) = "white" {}
		_MagnitudeWave("Magnitude Wave", Range(0,1)) = 1
		_MagnitudeDisplacement("Magnitude Displacement", Range(0,4)) = 1
		_Offset("Offset",  vector) = (1, -2, 0)
		_ColorTint ("Tint", Color) = (1, 1, 1, 1.0)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"PreviewType" = "Plane"
		}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _WaveTex;
			uniform float4 _WaveTex_ST; 
			sampler2D _DisplaceTex;
			uniform float4 _DisplaceTex_ST;

			float _MagnitudeWave;
			float _MagnitudeDisplacement;

			float4 _Offset;
      		fixed4 _ColorTint;


			float4 frag(v2f i) : SV_Target
			{
				float2 wave = tex2D(_WaveTex, i.uv + _WaveTex_ST + _Offset).xy  ;
				wave = ((wave * 1.5) - 1) * _MagnitudeWave;

				float2 disp = tex2D(_DisplaceTex, i.uv + _DisplaceTex_ST).xy  ;
				disp = ((disp * 1.5) - 1) * _MagnitudeDisplacement;


				float4 col = tex2D(_MainTex, i.uv + wave - disp);
				col *= _ColorTint;
				return col;
			}
			ENDCG
		}
	}
}
