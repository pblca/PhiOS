using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Display))]
public class DisplayEditor : Editor {

	public override void OnInspectorGUI(){	
		base.OnInspectorGUI ();

		if (!Application.isPlaying) {
			Display display = (Display)target;

			// force display height to 0 if auto calculating
			if (display.autoDisplayHeight && display.displayHeight != 0) {
				display.displayHeight = 0;
			}
		}
	}
}
