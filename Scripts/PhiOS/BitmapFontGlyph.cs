using UnityEngine;
using System.Collections;

public class BitmapFontGlyph {
	
	public string glyphString;
	public float x;
	public float y;
	public float xOffset;
	public float yOffset;
	public float width;
	public float height;
	public Vector3[] vertices = new Vector3[4];
	public Vector2[] uvs = new Vector2[4];

	public void RecalculateGlyphMetrics(float glyphWidth, float glyphHeight, float textureSize, float bleed){

		// calculate glyph vertices
		vertices[0] = new Vector3(0f + xOffset / glyphWidth, 0f + yOffset / glyphHeight, 0f);
		vertices[1] = new Vector3(1f * (width / glyphWidth) + xOffset / glyphWidth, 1f * (height / glyphHeight) + yOffset / glyphHeight, 0f);
		vertices[2] = new Vector3(1f * (width / glyphWidth) + xOffset / glyphWidth, 0f + yOffset / glyphHeight, 0f);
		vertices[3] = new Vector3(0f + xOffset / glyphWidth, 1f * (height / glyphHeight)  + yOffset / glyphHeight, 0f);

		// calculate glyph uvs
		uvs[0] = new Vector2((x / textureSize) + (bleed / textureSize), ((textureSize - (y + height)) / textureSize) + (bleed / textureSize));
		uvs[1] = new Vector2(((x + width) / textureSize) - (bleed / textureSize), ((textureSize - y) / textureSize) - (bleed / textureSize));
		uvs[2] = new Vector2(((x + width) / textureSize) - (bleed / textureSize), ((textureSize - (y + height)) / textureSize) + (bleed / textureSize));
		uvs[3] = new Vector2((x / textureSize) + (bleed / textureSize), ((textureSize - y) / textureSize) - (bleed / textureSize));
	}
}
