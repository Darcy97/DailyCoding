// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "H2/H_Dissolve_blend"
{
	Properties
	{
		[HDR]_Main_Color("Main_Color", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		_Brightness("Brightness", Float) = 1
		_Power("Power", Float) = 1
		_Alpha("Alpha", Float) = 1
		_Dissolve_Tex("Dissolve_Tex", 2D) = "white" {}
		_Dissolve_Range("Dissolve_Range", Float) = 0
		[HDR]_Range_Color("Range_Color", Color) = (1,1,1,1)
		_Range_Color_Level("Range_Color_Level", Float) = 1
		_Diss_amount("Diss_amount", Range( -0.01 , 1)) = -0.01
		_Diss_soft("Diss_soft", Range( 0.0 , 1)) = 0
		_MaskTex("MaskTex", 2D) = "white" {}
		[Toggle]_Particle_control("Particle_control", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		
		
		
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
			

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform float4 _Main_Color;
			uniform float _Brightness;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _Power;
			uniform float4 _Range_Color;
			uniform float _Range_Color_Level;
			uniform float _Dissolve_Range;
			uniform sampler2D _Dissolve_Tex;
			uniform float4 _Dissolve_Tex_ST;
			uniform float _Particle_control;
			uniform float _Diss_amount;
			uniform float _Alpha ,_Diss_soft;
			uniform sampler2D _MaskTex;
			uniform float4 _MaskTex_ST;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_color = v.color;
				o.ase_texcoord = v.ase_texcoord;
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
				float2 uv_MainTex = i.ase_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode2 = tex2D( _MainTex, uv_MainTex );
				float4 temp_cast_0 = (_Power).xxxx;
				float2 uv_Dissolve_Tex = i.ase_texcoord.xy * _Dissolve_Tex_ST.xy + _Dissolve_Tex_ST.zw;
				float4 tex2DNode45 = tex2D( _Dissolve_Tex, uv_Dissolve_Tex );
				float3 desaturateInitialColor47 = tex2DNode45.rgb;
				float desaturateDot47 = dot( desaturateInitialColor47, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar47 = lerp( desaturateInitialColor47, desaturateDot47.xxx, 1.0 );
				float3 temp_output_48_0 = ( desaturateVar47 * tex2DNode45.a );
				float4 uv0_Dissolve_Tex = i.ase_texcoord;
				uv0_Dissolve_Tex.xy = i.ase_texcoord.xy * _Dissolve_Tex_ST.xy + _Dissolve_Tex_ST.zw;
				float3 temp_cast_2 = (lerp(_Diss_amount,(-0.01 + (uv0_Dissolve_Tex.z - 0.0) * (1.0 - -0.01) / (1.0 - 0.0)),_Particle_control)).xxx;
				float3 temp_output_135_0 = ( 1.0 - smoothstep( ( ( _Dissolve_Range / 10.0 ) + temp_output_48_0 ) ,( ( _Dissolve_Range / 10.0 ) + temp_output_48_0 )+_Diss_soft, temp_cast_2 ) );
				float3 temp_cast_3 = (lerp(_Diss_amount,(-0.01 + (uv0_Dissolve_Tex.z - 0.0) * (1.0 - -0.01) / (1.0 - 0.0)),_Particle_control)).xxx;
				float2 uv_MaskTex = i.ase_texcoord.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				float3 desaturateInitialColor168 = tex2D( _MaskTex, uv_MaskTex ).rgb;
				float desaturateDot168 = dot( desaturateInitialColor168, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar168 = lerp( desaturateInitialColor168, desaturateDot168.xxx, 1.0 );
				float4 appendResult152 = (float4(( ( _Main_Color * i.ase_color * _Brightness * pow( tex2DNode2 , temp_cast_0 ) ) + ( ( tex2DNode2 * tex2DNode2.a ) * _Range_Color * _Range_Color_Level * 10.0 * float4( ( temp_output_135_0 - ( 1.0 - step( temp_output_48_0 , temp_cast_3 ) ) ) , 0.0 ) ) ).rgb , ( ( i.ase_color.a * tex2DNode2.a * _Alpha * temp_output_135_0 ) * desaturateVar168 ).x));
				
				
				finalColor = appendResult152;
				return finalColor;
			}
			ENDCG
		}
	}
//	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16800
205;371;1295;828;1155.448;841.0057;1.6;True;False
Node;AmplifyShaderEditor.SamplerNode;45;-1490.351,375.6263;Float;True;Property;_Dissolve_Tex;Dissolve_Tex;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;91;-1408.666,208.9335;Float;False;Constant;_Desaturate;Desaturate;11;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;47;-1114.386,204.8912;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;144;-813.3779,246.7064;Float;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;134;-837.0972,67.92621;Float;False;Property;_Dissolve_Range;Dissolve_Range;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;111;-1537.447,894.8558;Float;True;0;45;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;127;-1189.703,959.521;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.01;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;147;-1386.295,720.9472;Float;False;Property;_Diss_amount;Diss_amount;9;0;Create;True;0;0;False;0;-0.01;0;-0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;143;-603.8439,142.0805;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-840.0056,466.4367;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ToggleSwitchNode;146;-897.4589,822.8492;Float;False;Property;_Particle_control;Particle_control;11;0;Create;True;0;0;False;0;1;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;133;-502.1542,280.445;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StepOpNode;54;-359.1895,807.8577;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StepOpNode;132;-334.5314,449.185;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;2;-771.4116,-189.5113;Float;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;40;-297.1732,-189.2393;Float;False;Property;_Power;Power;3;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;135;-45.33772,439.8143;Float;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;55;-31.89209,806.7879;Float;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-300.743,-450.2086;Float;False;Property;_Brightness;Brightness;2;0;Create;True;0;0;False;0;1;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;89;-299.2369,-668.6363;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;43;-308.4906,-904.8239;Float;False;Property;_Main_Color;Main_Color;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0.5,0.5,0.5,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;137;344.9153,706.3158;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;130;-49.89844,94.90078;Float;False;Property;_Alpha;Alpha;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;167;869.5373,53.31736;Float;True;Property;_MaskTex;MaskTex;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;141;322.2477,206.7261;Float;False;Property;_Range_Color;Range_Color;7;1;[HDR];Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;39;-148.9098,-309.954;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;166;347.348,6.317509;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;164;355.362,489.3421;Float;False;Constant;_Float2;Float 2;11;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;142;303.7701,400.4711;Float;False;Property;_Range_Color_Level;Range_Color_Level;8;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;139;652.4502,215.5028;Float;False;5;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DesaturateOpNode;168;1258.383,-59.69688;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;349.6536,-167.808;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;348.2342,-391.9144;Float;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;169;1503.113,-157.2824;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;153;903.963,-390.3255;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;152;1728.134,-250.5531;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;148;2015.504,-250.8296;Float;False;True;2;Float;ASEMaterialInspector;0;1;H2/H_Dissolve_blend;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;0;False;-1;True;False;0;False;-1;0;False;-1;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;False;False;False;False;False;False;False;False;False;True;0;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;47;0;45;0
WireConnection;47;1;91;0
WireConnection;127;0;111;3
WireConnection;143;0;134;0
WireConnection;143;1;144;0
WireConnection;48;0;47;0
WireConnection;48;1;45;4
WireConnection;146;0;147;0
WireConnection;146;1;127;0
WireConnection;133;0;143;0
WireConnection;133;1;48;0
WireConnection;54;0;48;0
WireConnection;54;1;146;0
WireConnection;132;0;133;0
WireConnection;132;1;146;0
WireConnection;135;0;132;0
WireConnection;55;0;54;0
WireConnection;137;0;135;0
WireConnection;137;1;55;0
WireConnection;39;0;2;0
WireConnection;39;1;40;0
WireConnection;166;0;2;0
WireConnection;166;1;2;4
WireConnection;139;0;166;0
WireConnection;139;1;141;0
WireConnection;139;2;142;0
WireConnection;139;3;164;0
WireConnection;139;4;137;0
WireConnection;168;0;167;0
WireConnection;63;0;89;4
WireConnection;63;1;2;4
WireConnection;63;2;130;0
WireConnection;63;3;135;0
WireConnection;3;0;43;0
WireConnection;3;1;89;0
WireConnection;3;2;44;0
WireConnection;3;3;39;0
WireConnection;169;0;63;0
WireConnection;169;1;168;0
WireConnection;153;0;3;0
WireConnection;153;1;139;0
WireConnection;152;0;153;0
WireConnection;152;3;169;0
WireConnection;148;0;152;0
ASEEND*/
//CHKSM=8F5A0927B6896B36D64659F50A2C07703D16B27F