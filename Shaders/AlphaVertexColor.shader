Shader "Phigames/Alpha Vertex Color" {
	Properties {
	    _Color ("Color", Color) = (1,1,1,1)
	    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader {
	    ZWrite Off
	    Alphatest Greater 0
	    Tags {Queue=Transparent}
	    Blend SrcAlpha OneMinusSrcAlpha 
	    ColorMask RGB
	    Pass {
	        ColorMaterial AmbientAndDiffuse
	        Lighting Off
	        SeparateSpecular Off
	        SetTexture [_MainTex] {
	            Combine texture * primary, texture * primary
	        }
	        SetTexture [_MainTex] {
	            constantColor [_Color]
	            Combine previous * constant DOUBLE, previous * constant
	        } 
	    }
	}
}