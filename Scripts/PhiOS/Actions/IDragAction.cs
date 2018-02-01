using UnityEngine;
using System.Collections;

public interface IDragAction {
	void OnDragStart();
	void OnDragDelta (Vector2 delta);
}
