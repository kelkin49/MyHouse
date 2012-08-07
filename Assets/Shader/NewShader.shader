Shader "Electricity using NO Surface Shader" {

    Properties {

        _Color ("Main Color", Color) = (1,1,1,1)

        _MainTex ("Base (RGB)", 2D) = "white" {}

        _Noise ("Noise (RGB)", 2D) = "white" {}

        _Ramp ("Ramp (RGBA)", 2D) = "white" {}

        _Speed ("Scroll Speed", float) = 0.1

        _FallOff("FallOff", float) = 0.85

        _Width("Width", float) = 0.2

        _OutlineColor ("Outline Color", Color) = (1,1,1,1)

        _OutlineColorFallOff("Outline Color FallOff", float) = 1.1

    }

    SubShader {

        /* surface debug info:*/

        Pass {

            Name "FORWARD"

            Tags { "LightMode" = "ForwardBase" }

// shader source for this pass:

CGPROGRAM

#pragma vertex vert_surf

#pragma fragment frag_surf

#pragma fragmentoption ARB_precision_hint_fastest

#pragma multi_compile_fwdbase#include "HLSLSupport.cginc"

#include "UnityCG.cginc"

#include "Lighting.cginc"

#include "AutoLight.cginc"

 

#define INTERNAL_DATA

#define WorldReflectionVector(data,normal) data.worldRefl

#line 14

 

        //#pragma surface surf Lambert

        #pragma debug

        sampler2D _MainTex;

 

        struct Input {

            float2 uv_MainTex;

        };

 

        void surf (Input IN, inout SurfaceOutput o) {

            half4 c = tex2D (_MainTex, IN.uv_MainTex);

            o.Albedo = c.rgb;

            o.Alpha = c.a;

        }

        struct v2f_surf {

  float4 pos : SV_POSITION;

  float2 hip_pack0 : TEXCOORD0;

  #ifdef LIGHTMAP_OFF

  float3 normal : TEXCOORD1;

  #endif

  #ifndef LIGHTMAP_OFF

  float2 hip_lmap : TEXCOORD2;

  #else

  float3 vlight : TEXCOORD2;

  #endif

  LIGHTING_COORDS(3,4)

};

#ifndef LIGHTMAP_OFF

float4 unity_LightmapST;

#endif

float4 _MainTex_ST;

v2f_surf vert_surf (appdata_full v) {

  v2f_surf o;

  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);

  o.hip_pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);

  #ifndef LIGHTMAP_OFF

  o.hip_lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;

  #endif

  float3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);

  #ifdef LIGHTMAP_OFF

  o.normal = worldN;

  #endif

  #ifdef LIGHTMAP_OFF

  float3 shlight = ShadeSH9 (float4(worldN,1.0));

  o.vlight = shlight;

  #ifdef VERTEXLIGHT_ON

  float3 worldPos = mul(_Object2World, v.vertex).xyz;

  o.vlight += Shade4PointLights (

    unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,

    unity_LightColor0, unity_LightColor1, unity_LightColor2, unity_LightColor3,

    unity_4LightAtten0, worldPos, worldN );

  #endif // VERTEXLIGHT_ON

  #endif // LIGHTMAP_OFF

  TRANSFER_VERTEX_TO_FRAGMENT(o);

  return o;

}

#ifndef LIGHTMAP_OFF

sampler2D unity_Lightmap;

#endif

half4 frag_surf (v2f_surf IN) : COLOR {

  Input surfIN;

  surfIN.uv_MainTex = IN.hip_pack0.xy;

  SurfaceOutput o;

  o.Albedo = 0.0;

  o.Emission = 0.0;

  o.Specular = 0.0;

  o.Alpha = 0.0;

  o.Gloss = 0.0;

  #ifdef LIGHTMAP_OFF

  o.Normal = IN.normal;

  #endif

  surf (surfIN, o);

  half atten = LIGHT_ATTENUATION(IN);

  half4 c;

  #ifdef LIGHTMAP_OFF

  c = LightingLambert (o, _WorldSpaceLightPos0.xyz, atten);

  c.rgb += o.Albedo * IN.vlight;

  #else // LIGHTMAP_OFF

  half3 lmFull = DecodeLightmap (tex2D(unity_Lightmap, IN.hip_lmap.xy));

  #ifdef SHADOWS_SCREEN

  c.rgb = o.Albedo * min(lmFull, atten*2);

  #else

  c.rgb = o.Albedo * lmFull;

  #endif

  c.a = o.Alpha;

#endif // LIGHTMAP_OFF

  return c;

}

 

ENDCG

        }

        

        // lightning

        Pass{

            Tags {"Queue" = "Transparent" }

            Cull Off

            Lighting Off

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert

            #pragma fragment frag

            

            sampler2D _Noise;

            float4 _Noise_ST;

            sampler2D _Ramp;

            float _Speed;

            float _FallOff;

            float _Width;

            half4 _OutlineColor;

            float _OutlineColorFallOff;

            

            struct data{

                float4 vertex : POSITION;

                float4 texcoord : TEXCOORD0;

                float3 normal : NORMAL;

            };

            

            struct v2f{

                float4 position : POSITION;

                float2 uv : TEXCOORD0;

                float viewDir : TEXCOORD1;

            };

            

            v2f vert(data i){

                v2f o;

                float4 vertex = i.vertex + float4(i.normal * _Width, 0);

                o.position = mul(UNITY_MATRIX_MVP, vertex);

                o.uv = TRANSFORM_TEX(i.texcoord, _Noise);

                o.viewDir = 1 - abs(dot(i.normal, normalize(ObjSpaceViewDir(vertex)))); 

                return o;

            }

            

            half4 frag(v2f i) : COLOR{

                half4 noise1 = tex2D(_Noise, i.uv + _Time.xy * _Speed);

                half4 noise2 = tex2D(_Noise, i.uv - _Time.yw * _Speed);

                float x = pow(i.viewDir, _FallOff)*(dot(noise1, noise2));

                half4 col = tex2D(_Ramp, float2(x, 0));

                _OutlineColor.a *= pow(i.viewDir, _OutlineColorFallOff);

                return col+_OutlineColor;

            }

            

            ENDCG

        }

    }

}