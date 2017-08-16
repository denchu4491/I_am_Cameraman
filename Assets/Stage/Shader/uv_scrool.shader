Shader "Custom/uv_scrool" {
	Properties{
		_MainTex("Water Texture", 2D) = "white" {}
		_Speed_x("UV Speed x",Range(0,10)) = 2
		_Speed_y("UV Speed y",Range(0,10)) = 1
		_Alpha("Texture Alpha",Range(0,1)) = 1
	}
		SubShader{
		Tags{ 
			"Queue"="Transparent"
			"RenderType" = "Transparent" 
		}
		LOD 200

		CGPROGRAM
#pragma surface surf Standard fullforwardshadows alpha
#pragma target 3.0

		sampler2D _MainTex;
		float _Speed_x;
		float _Speed_y;
		float _Alpha;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutputStandard o) {
		fixed2 uv = IN.uv_MainTex;
		uv.x += _Speed_x * _Time;
		uv.y += _Speed_y * _Time;
		o.Albedo = tex2D(_MainTex, uv);
		o.Alpha = _Alpha;
	}
	ENDCG
	}
		FallBack "Transparent/Diffuse"
}