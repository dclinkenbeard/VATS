/*Please do support www.bitshiftprogrammer.com by joining the facebook page : fb.com/BitshiftProgrammer
Legal Stuff:
This code is free to use no restrictions but attribution would be appreciated.
Any damage caused either partly or completly due to usage of this stuff is not my responsibility*/
Shader "BitShiftProgrammer/AnimatedFishHammerhead"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_EffectRadius("Wave Effect Radius",Range(0.0,1.0)) = 0.5
		_WaveSpeed("Wave Speed", Range(0.0,100.0)) = 3.0
		_WaveHeight("Wave Height", Range(0.0,30.0)) = 5.0
		_WaveDensity("Wave Density", Range(0.0001,1.0)) = 0.007
		_Yoffset("Y Offset",Float) = 0.0
		_Threshold("Threshold",Range(0,30)) = 3
		_StrideSpeed("Stride Speed",Range(0.0,10.0)) = 2.0
		_StrideStrength("Stride Strength", Range(0.0,20.0)) = 3.0
		_MoveOffset("Move Offset",Float) = 0.0
	}
		SubShader
		{
			Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
			Cull Off
			Blend SrcAlpha OneMinusSrcAlpha
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
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
				half _EffectRadius;
				half _WaveSpeed;
				half _WaveHeight;
				half _WaveDensity;
				half _Yoffset;
				int _Threshold;
				half _StrideSpeed;
					half _StrideStrength;
					half _MoveOffset;

				v2f vert(appdata v)
				{
					v2f o;
					half sinUse = sin(-_Time.y * _WaveSpeed + _MoveOffset + v.vertex.x * _WaveDensity);
					half yValue = v.vertex.x - _Yoffset;
					half yDirScaling = clamp(pow(yValue * _EffectRadius,_Threshold),0.0,1.0);
					v.vertex.z = v.vertex.z + sinUse * _WaveHeight * yDirScaling;
					v.vertex.z = v.vertex.z + sin(-_Time.y * _StrideSpeed + _MoveOffset) * _StrideStrength;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex,i.uv);
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG
			}
		}
}