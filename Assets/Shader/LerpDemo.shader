Shader "Custom/LerpDemo"
{
    Properties
    {
        _Sand("_Sand", 2D) = "black" {}
        _TerrainFloor("_TerrainFloor", 2D) = "black" {}
        _Blend("_Blend", Range(0,1) ) = 0.5
    }

    SubShader
    {
        Tags
        {
            "Queue"="Geometry"
            "IgnoreProjector"="False"
            "RenderType"="Opaque"
        }

        Cull Back
        ZWrite On
        ZTest LEqual
        ColorMask RGBA

        CGPROGRAM
        #pragma surface surf BlinnPhongEditor  
        #pragma target 2.0

        sampler2D _TerrainFloor;
        sampler2D _Sand;
        float _Blend;

        struct EditorSurfaceOutput {
            half3 Albedo;
            half3 Normal;
            half3 Emission;
            half3 Gloss;
            half Specular;
            half Alpha;
            half4 Custom;
        };

        inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
        {
            half3 spec = light.a * s.Gloss;
            half4 c;
            c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
            c.a = s.Alpha;
            return c;
        }

            inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
            {
                half3 h = normalize (lightDir + viewDir);

                half diff = max (0, dot ( lightDir, s.Normal ));

                float nh = max (0, dot (s.Normal, h));
                float spec = pow (nh, s.Specular*128.0);

                half4 res;
                res.rgb = _LightColor0.rgb * diff;
                res.w = spec * Luminance (_LightColor0.rgb);
                res *= atten * 2.0;

                return LightingBlinnPhongEditor_PrePass( s, res );
            }

            struct Input {
                float2 uv_Sand;
                float2 uv_TerrainFloor;
            };

            void surf (Input IN, inout EditorSurfaceOutput o)
            {
                o.Normal = float3(0.0,0.0,1.0);
                o.Alpha = 1.0;
                o.Albedo = 0.0;
                o.Emission = 0.0;
                o.Gloss = 0.0;
                o.Specular = 0.0;
                o.Custom = 0.0;

                float4 Sampled2D1=tex2D(_Sand,IN.uv_Sand.xy);
                float4 Sampled2D0=tex2D(_TerrainFloor,IN.uv_TerrainFloor.xy);
                float4 Lerp0=lerp(Sampled2D1,Sampled2D0,_Blend.x);

                o.Albedo = Lerp0;

                o.Normal = normalize(o.Normal);
            }
        ENDCG
    }
    Fallback "Diffuse"
}