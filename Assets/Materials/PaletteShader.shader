Shader "Unlit/PaletteShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Palette ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha
        
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjection"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float2 uv : TEXCOORD0;
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _Palette;

            v2f vert (const appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (const v2f i) : SV_Target
            {
                const float2 pos = tex2D(_MainTex, i.uv).rg;
                return tex2D(_Palette, pos);
            }
            ENDCG
        }
    }
}
