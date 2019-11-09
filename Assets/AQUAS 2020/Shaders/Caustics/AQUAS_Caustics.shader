Shader "AQUAS/Misc/Caustics"
{
	Properties
	{
		_CausticsScale("Caustics Scale", Float) = 2
		_WaterLevel("Water Level", Float) = 0
		_Texture("Texture", 2D) = "white" {}
		_Intensity("Intensity", Float) = 0
		_DistanceVisibility("Distance Visibility", Float) = 0
		_Fade("Fade", Float) = 0
		_DepthFade("Depth Fade", Float) = 0
	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Overlay" }
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		GrabPass{ }

		Pass
		{
			Name "Unlit"
			
			CGPROGRAM

#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
		//only defining to not throw compilation error over Unity 5.5
		#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityShaderVariables.cginc"
			#include "AutoLight.cginc"
			#include "UnityStandardBRDF.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float3 ase_normal : NORMAL;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
			};

			//This is a late directive
			
			UNITY_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
			uniform sampler2D _Texture;
			uniform float _CausticsScale;
			uniform float _WaterLevel;
			uniform float _DepthFade;
			uniform float _Intensity;
			uniform float _DistanceVisibility;
			uniform float _Fade;
			inline float4 ASE_ComputeGrabScreenPos( float4 pos )
			{
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				float4 o = pos;
				o.y = pos.w * 0.5f;
				o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
				return o;
			}
			
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord = screenPos;
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord1.xyz = ase_worldPos;
				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord2.xyz = ase_worldNormal;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.w = 0;
				o.ase_texcoord2.w = 0;
				float3 vertexValue =  float3(0,0,0) ;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				float4 screenPos = i.ase_texcoord;
				float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( screenPos );
				float4 screenColor53 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,ase_grabScreenPos/ase_grabScreenPos.w);
				float3 ase_worldPos = i.ase_texcoord1.xyz;
				#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
				float4 ase_lightColor = 0;
				#else //aselc
				float4 ase_lightColor = _LightColor0;
				#endif //aselc
				float3 worldSpaceLightDir = Unity_SafeNormalize(UnityWorldSpaceLightDir(ase_worldPos));
				float3 ase_worldNormal = i.ase_texcoord2.xyz;
				float3 normalizedWorldNormal = normalize( ase_worldNormal );
				float dotResult62 = dot( worldSpaceLightDir , normalizedWorldNormal );
				float4 lerpResult63 = lerp( ( saturate( ( screenColor53 * tex2D( _Texture, ( (ase_worldPos).xz * float2( 0.1,0.1 ) * _CausticsScale ) ) * float4( ase_lightColor.rgb , 0.0 ) ) ) * ( 1.0 - saturate( ( ( ( _WaterLevel + -1.0 ) * -1.0 ) + ase_worldPos.y ) ) ) * saturate( ( ase_worldPos.y + ( -1.0 * _DepthFade ) ) ) * _Intensity * saturate( dotResult62 ) ) , float4(0,0,0,1) , saturate( pow( ( distance( ase_worldPos , _WorldSpaceCameraPos ) / _DistanceVisibility ) , _Fade ) ));
				
				
				finalColor = lerpResult63;
				return finalColor;
			}
			ENDCG
		}
	}
}