using UnityEngine;
using System.Collections;

public class CellFades {
	public static string DEFAULT_REVERSE = "░▒▓█";
	public static string DEFAULT = "█▓▒░";
}

public class Cell {

	public int layer;
	public Vector2 position;
	public MonoBehaviour owner;
	public string content = "";
	public Color backgroundColor;
	public Color color;
	public IHoverAction hoverAction;
	public IClickAction clickAction;
	public IDragAction dragAction;
	public IScrollAction scrollAction;

	private string targetContent = "";
	private Color targetColor;
	private float fadeLeft = 0f;
	private float fadeMax = 0f;
	private Color fadeColor;
	private string fades = "";
	private bool fadeFinished = true;

	public void Clear(){
		SetContent("", Color.clear, Color.clear);
	}

	public void Clear(float fadeTime, Color fadeColor){
		SetContent("", Color.clear, Color.clear, fadeTime, fadeColor, CellFades.DEFAULT_REVERSE);
	}

	public void SetContent(string content, Color backgroundColor, Color color){
		SetContent(content, backgroundColor, color, 0f,	color, "");
	}

	public void SetContent(string content, Color backgroundColor, Color color, float fadeTime, Color fadeColor){
		SetContent(content, backgroundColor, color, fadeTime, fadeColor, CellFades.DEFAULT);
	}

	public void SetContent(
		string content,
		Color backgroundColor,
		Color color,
		float fadeMax,
		Color fadeColor,
		string fades){

		// set target content and color
		targetContent = content;
		this.backgroundColor = backgroundColor;
		targetColor = color;

		// fade
		if (fadeMax > 0f) {
			this.fadeLeft = this.fadeMax = Random.Range (0f, fadeMax);
			this.color = this.fadeColor = fadeColor;
			this.fades = fades;
			fadeFinished = false;
		}

		// instant
		else {
			this.fadeLeft = 0f;
			this.fadeMax = 0f;
			fadeFinished = false;
		}

		// add cell to top layer
		if (targetContent != "") {
			Display.GET.AddCellAsTopLayer(this);	
		}
	}

	public void Update(){

		// display initialized
		if (Display.GET.initialized) {

			// fade
			if (fadeLeft > 0f) {
				content = targetContent.Trim().Length > 0 || content.Trim().Length > 0 ? 
					fades.Substring(Mathf.RoundToInt((fadeLeft / fadeMax) * (fades.Length - 1)), 1) :
					targetContent;
				color = Color.Lerp(
					targetColor, 
					fadeColor, 
					Display.GET.colorLerpCurve.Evaluate(fadeLeft / fadeMax));
				fadeLeft -= Time.deltaTime;
			}

			// fade finished
			else {

				// remove cell from top layer
				if (!fadeFinished && targetContent == "") {
					Display.GET.RemoveCellAsTopLayer(this);
				}

				fadeFinished = true;
				fadeLeft = 0f;
				content = targetContent;
				color = targetColor;
			}
		}
	}
}
