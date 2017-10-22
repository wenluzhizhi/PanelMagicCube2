Shader "Custom/moveImages" {
	properties{
	   _MainTex("MainTexture",2D)="white"{}
	   _NoiseTexture("NoiseTexture",2D)="white"{}
	    _NoiseTexture2("NoiseTexture",2D)="white"{}
	}
	Subshader{

	    pass{

	        CGPROGRAM
	        #pragma vertex vert
	        #pragma fragment frag

	        #include "Unitycg.cginc"


	        struct a2v{
	          float4 vertex:POSITION;
	          float4 texcoord:TEXCOORD;


	        };


	        struct v2f{
	           float4 pos:SV_POSITION;
	           float2 uv:TEXCOORD0;
	        };

	        v2f vert(a2v v){
	           v2f o;
	           o.pos=mul(UNITY_MATRIX_MVP,v.vertex);
	           o.uv=v.texcoord.xy;
	           return o;
	        }
	        sampler2D _MainTex;
	        sampler2D _NoiseTexture;
	        sampler2D _NoiseTexture2;
	        fixed4 frag(v2f i):SV_Target{
	          fixed4 col=fixed4(1,1,1,1);

	          float2 uv2=i.uv;
	          uv2.x+=sin(_Time.x);
	          uv2.y+=cos(_Time.x);

	          float2 uv3=i.uv;
	          uv3-=sin(_Time.x);
	          float r=tex2D(_NoiseTexture,uv2).r;
	          float r2=tex2D(_NoiseTexture2,uv3).g;

	          
	          col=tex2D(_MainTex,i.uv);
	          r=r*r2*2;
	          col.rgb=col.rgb*(1-r)+col.rbg*r;
	          return col;
	        }

	        ENDCG
	    }
	}
}
