// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "light_mask"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}

			SubShader
			{
				Tags
				{
					"Queue" = "Geometry"
					"IgnoreProjector" = "True"
					"RenderType" = "TransparentCutout"
					"PreviewType" = "Plane"
					"CanUseSpriteAtlas" = "True"
		            "LightMode" = "ForwardAdd"
				}

				Cull Off
				Lighting Off
				ZWrite Off
				Blend One OneMinusSrcAlpha

				CGPROGRAM
				#pragma surface surf Carter vertex:vert nofog keepalpha noinstancing addshadow fullforwardshadows
				#pragma multi_compile _ PIXELSNAP_ON
				#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
				#include "UnitySprites.cginc"
				#include "UnityPBSLighting.cginc"
				

				sampler2D _Mask;
				float4 _LightColorO;

				struct Input
				{
					float2 uv_MainTex;
					fixed4 color;
				};

				half4 LightingCarter(SurfaceOutputStandard s, half3 viewDir, UnityGI gi)
				{
					//half alpha = s.Alpha * (gi.light.color.r + gi.light.color.g + gi.light.color.b);
					
					return float4(s.Albedo.r,s.Albedo.g,s.Albedo.b,s.Alpha);//LightingStandard(s,viewDir,gi);
				}

				void LightingCarter_GI(SurfaceOutputStandard s, UnityGIInput data, inout UnityGI gi)
				{
					LightingStandard_GI(s, data, gi);
				}

				void vert(inout appdata_full v, out Input o)
				{
					v.vertex = UnityFlipSprite(v.vertex, _Flip);

					#if defined(PIXELSNAP_ON)
					v.vertex = UnityPixelSnap(v.vertex);
					#endif

					UNITY_INITIALIZE_OUTPUT(Input, o);
					o.color = v.color * _Color;
					
					o.color.a = 1;// (_LightColor0.r + _LightColor0.g + _LightColor0.b) * v.color.a * proxy / 3.0;
				}

				void surf(Input IN, inout SurfaceOutputStandard o)
				{
					fixed4 c = tex2D(_MainTex	, IN.uv_MainTex) * IN.color;
					o.Albedo = c.rgb;
					o.Alpha = c.a;
					


				}
				ENDCG
			}

	}