using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour {
	public FieldClickHandler Target;
	
	void Start(){
		if(Target != null){
			Target.OnClick.AddListener(OnClick);
		}
	}
	
	void OnClick(FieldCell cell){
		cell.DestroyInside();
	}
}
