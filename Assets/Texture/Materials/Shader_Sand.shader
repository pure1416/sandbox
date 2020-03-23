Shader "Custom/Shader_Sand"
{
    Properties
    {
		_MainTex ("Sand Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;	//メインテクスチャ
		

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed2 uv = IN.uv_MainTex;
			uv.x += 0.4f * _Time;	//X
			uv.y += 0.4f * _Time;	//Z

			o.Albedo = tex2D(_MainTex, uv);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
