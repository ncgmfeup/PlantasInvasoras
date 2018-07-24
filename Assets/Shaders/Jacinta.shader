// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shaders/Jacinta"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_AuraTex("Aura Texture", 2D) = "white" {}
		_EvilAuraColor ("Evil Aura Color", Color) = (1,1,1,1)
		_EvilAuraForce ("Evil Aura Force",  Range(0.0,1.0)) = 0.5
		_DecayAuraColor ("Decay Aura Color", Color) = (1,1,1,1)
		_DecayAuraForce ("Decay Aura Force",  Range(0.0,1.0)) = 0.5
		_AuraPriority ("Decay Aura Force", Range(0.0,1.0)) = 0
		_ScrollXSpeed ("X Scroll Speed", Range(-5, 5)) = 0
  	_ScrollYSpeed ("Y Scroll Speed", Range(-5, 5)) = 0
	}
	
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
		}


		Pass
		{
			Name "BASE"
			ZWrite Off
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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _AuraTex;
			
			float4 _EvilAuraColor;
			float _EvilAuraForce;
			float4 _DecayAuraColor;
			float _DecayAuraForce;
			
			float _AuraPriority;
			
			fixed _ScrollXSpeed;
      fixed _ScrollYSpeed;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				return o;
			}


			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				
				//Evil Aura
				float4 evil = _EvilAuraColor;

				fixed varX = _ScrollXSpeed * _Time;
        fixed varY = _ScrollYSpeed * _Time;
        fixed2 uv_Tex = i.uv + fixed2(varX, varY);
				evil *= tex2D(_AuraTex, uv_Tex);		

				//Evil Aura
				float4 decay = _DecayAuraColor;


				if (_AuraPriority < 0.5) {
					col.rgb = lerp(col, decay, _DecayAuraForce).rgb;
					col.rgb = lerp(col, evil, _EvilAuraForce).rgb;
				}	else {
					col.rgb = lerp(col, evil, _EvilAuraForce).rgb;
					col.rgb = lerp(col, decay, _DecayAuraForce).rgb;
				}
				
				return col;
			}
			
			ENDCG
		}
	}

}
