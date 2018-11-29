Shader "Sprite/Visible2D"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
        _MaxLight ("LightAtVisible", Range(0.01,10)) = 0.0
        _RevealTime ("reveal_time",Float) = 0.0
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
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        CGPROGRAM
        #pragma  surface surf Lambert vertex:vert nofog keepalpha noinstancing addshadow fullforwardshadows
        #pragma multi_compile _ PIXELSNAP_ON
        #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
        #include "UnitySprites.cginc"

        half _MaxLight;
        half _RevealTime;

        struct Input
        {
            float2 uv_MainTex;
            fixed4 color;
        };

        void vert (inout appdata_full v, out Input o)
        {
            v.vertex = UnityFlipSprite(v.vertex, _Flip);

            #if defined(PIXELSNAP_ON)
            v.vertex = UnityPixelSnap (v.vertex);
            #endif

            UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color * _Color;
			float l = 0.3086 * v.color.r + 0.6094 * v.color.g + 0.0820 * v.color.b;
			
			o.color.a = pow(length(_LightColor0) * v.color.a,2);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            _RevealTime -= unity_DeltaTime;
			//float intens = (_LightColor0.x + _LightColor0.y + _LightColor0.z);
            fixed4 c = SampleSpriteTexture(IN.uv_MainTex) * IN.color;
            float a = clamp(_RevealTime,0,1);
            //if(a < .1) a = 0;
			o.Albedo = c.rgb * c.a;
			o.Alpha = c.a;
			
				
		}
        ENDCG
    }

}