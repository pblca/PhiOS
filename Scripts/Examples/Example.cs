using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour, IClickAction, IHoverAction {

	public IEnumerator Start(){

		// wait until display has initialized
		while (!Display.IsInitialized()) {
			yield return null;
		}

		// output red hello world at 10,5 on layer 0
		string helloWorld = "Hello World!";
		for (int x = 0; x < helloWorld.Length; x++) {
			Cell cell = Display.CellAt(0, 10 + x, 5);
			cell.SetContent(
				helloWorld.Substring(x, 1),
				Color.clear,
				Color.red);
		}

		// output red hello world at 10,7 with green background on layer 0
		for (int x = 0; x < helloWorld.Length; x++) {
			Cell cell = Display.CellAt(0, 10 + x, 7);
			cell.SetContent(
				helloWorld.Substring(x, 1),
				Color.green,
				Color.red);
		}

		// output blue instruction at 0,10 with black background on layer 2
		string instruction = "Click to clear layer 1";
		for (int x = 0; x < instruction.Length; x++) {
			Cell cell = Display.CellAt(2, x, 10);
			cell.SetContent(
				instruction.Substring(x, 1),
				Color.black,
				Color.blue);
		}

		// add clickable cell text on layer 2
		string clickableText = "Click->";
		for (int x = 0; x < clickableText.Length; x++) {
			Cell cell = Display.CellAt(2, x, 17);
			cell.SetContent(
				clickableText.Substring(x, 1),
				Color.clear,
				Color.yellow);
		}

		// add clickable cell on layer 3
		Cell clickable = Display.CellAt(3, 7, 17);
		clickable.SetContent(
			"?",
			Color.black,
			Color.yellow);
		clickable.clickAction = this;
		clickable.hoverAction = this;

		StartCoroutine(RandomGrid());
		StartCoroutine(Transition());
	}

	public void Update(){

		// clear every cell on layer 1 with mouse click
		if (Input.GetMouseButtonDown(0)) {
			for (int x = 0; x < Display.GetDisplayWidth(); x++) {
				for (int y = 0; y < Display.GetDisplayHeight(); y++) {
					Cell cell = Display.CellAt(1, x, y);
					cell.Clear();
				}
			}
		}
	}

	public IEnumerator RandomGrid(){
		
		// fill random cells every frame on layer 1
		while (Application.isPlaying) {
			for (int i = 0; i < 50; i++) {
				Cell cell = Display.CellAt(
					1,
					Random.Range(17, Display.GetDisplayWidth()),
					Random.Range(0, Display.GetDisplayHeight()));

				// random color and alpha
				Color color = Color.Lerp(Color.yellow, Color.green, Random.Range(0f, 1f));
				color = new Color(color.r, color.g, color.b, Random.Range(0f, 1f));

				cell.SetContent(
					Random.Range(0, 10) + "",
					Color.clear,
					color);
			}

			yield return null;
		}
	}

	public IEnumerator Transition(){

		// repeat transition animation
		while (Application.isPlaying) {
			
			// output white text at 0,15 on layer 2 with fade transition
			string text = "Transition Animation";
			for (int x = 0; x < text.Length; x++) {
				Cell cell = Display.CellAt(2, x, 15);
				cell.SetContent(
					text.Substring(x, 1),
					Color.clear,
					Color.white,
					0.5f,
					Color.white);
			}

			// clear text after a while with red fade transition
			yield return new WaitForSeconds(0.75f);
			for (int x = 0; x < text.Length; x++) {
				Cell cell = Display.CellAt(2, x, 15);
				cell.Clear(
					0.5f, 
					Color.red);
			}

			yield return new WaitForSeconds(0.75f);
		}
	}

	#region IClickAction implementation

	public void OnMouseDown()
	{
		// show clicked text
		string clicked = "You Clicked!";
		for (int x = 0; x < clicked.Length; x++) {
			Cell cell = Display.CellAt(2, x, 19);
			cell.SetContent(
				clicked.Substring(x, 1),
				Color.clear,
				Color.red);
		}
	}

	#endregion

	#region IHoverAction implementation

	public void OnHoverEnter()
	{
		// show hover text
		string hover = "You Hovered!";
		for (int x = 0; x < hover.Length; x++) {
			Cell cell = Display.CellAt(2, x, 18);
			cell.SetContent(
				hover.Substring(x, 1),
				Color.clear,
				Color.white);
		}
	}

	public void OnHoverExit()
	{		
	}

	#endregion
}
