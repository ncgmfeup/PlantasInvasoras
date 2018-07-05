// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shaders/PlantAuraShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_AuraTex("Aura Texture", 2D) = "white" {}
		_AuraColor ("Aura Color", Color) = (1,1,1,1)
		_AuraForce ("Aura Forse",  Range(0.0,1.0)) = 0.5
		_ScrollXSpeed ("X Scroll Speed", Range(-5, 5)) = 0
  	_ScrollYSpeed ("Y Scroll Speed", Range(-5, 5)) = 0
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
			Name "BASE"
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
			float4 _AuraColor;
			float _AuraForce;
			float4 _AuraTex_ST;
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
				float4 aura = _AuraColor;
				//aura *= tex2D(_AuraTex, i.uv + _AuraTex_ST);				

				fixed varX = _ScrollXSpeed * _Time;
        fixed varY = _ScrollYSpeed * _Time;
        fixed2 uv_Tex = i.uv + fixed2(varX, varY);
				aura *= tex2D(_AuraTex, uv_Tex);		

				fixed4 col = tex2D(_MainTex, i.uv);
				col.rgb = lerp(col, aura, _AuraForce).rgb;
				return col;
			}
			
			ENDCG
		}
	}

}
