using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FieldClickEvent : UnityEvent<FieldCell> {}

public class FieldClickHandler : MonoBehaviour {
	public FieldCell Target;
	public FieldClickEvent OnClick;
	
	void Update(){
		if(Input.GetMouseButtonUp(0)){
			foreach(Camera i in Camera.allCameras){
				Vector2 orig = Input.mousePosition;
				Vector2 point = i.ScreenToWorldPoint(orig);
				if(Target.RoundedX == Mathf.RoundToInt(point.x) &&
				   Target.RoundedY == Mathf.RoundToInt(point.y)){
					OnClick.Invoke(Target);
					break;
				}
			}
		}
	}
}
