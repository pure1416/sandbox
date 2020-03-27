Shader "Unlit/Test"
{
    Properties
    {
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_RotateSpeed("Rotate Speed", float) = 1.0
		_Tiling("Tiling" , float) = 1.0
		_Angle("Angle" , float) = 0.0
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

			#define PI 3.141592		//π

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

            sampler2D _MainTex;		//テクスチャ
            float4 _MainTex_ST;	//?
			float _RotateSpeed;	//回転速度
			float _Tiling;		//タイリング

			float _Angle;		//角度(テスト用)

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
				//Timeを入力として、現在の回転角度(ラジアン)を作る
				half angle = frac(_Time.x) * PI * 2;
				//回転行列の素　回転量(引数)＝回転角度＊回転速度(回転角度はラジアン角)
				half AngleCos = cos(_Angle * _RotateSpeed);
				half AngleSin = sin(_Angle * _RotateSpeed);
				//2次元の回転行列
				half2x2 RotateMatrix = half2x2(AngleCos, -AngleSin, AngleSin, AngleCos);
				//タイリング処理
				i.uv = frac(i.uv * _Tiling);
				//中心を起点としてUVを回転させる
				i.uv = mul(i.uv - 0.5f, RotateMatrix) + 0.5f;

				//ここまででUV座標を計算した

				//角度で向きを決め、スピードでスクロール速度を決めてy方向でスクロール
				//あとは色。書くのも一つの手。

				i.uv.y += 0.1f * _Time.y;

                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
