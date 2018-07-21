// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shaders/Overlay"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OverlayTex("Overlay Texture", 2D) = "white" {}
		_OverlayBlend("Mask blending", Range(0.0,1.0)) = 0.5
	}
	
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _OverlayTex;
			
			float _OverlayBlend;
		
			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed4 base = tex2D(_MainTex, i.uv);
				fixed4 overlay = tex2D(_OverlayTex, i.uv);
				fixed4 col = lerp(base, overlay, _OverlayBlend);
 				
				return col;
			}
			
			ENDCG
		}
	}

}
