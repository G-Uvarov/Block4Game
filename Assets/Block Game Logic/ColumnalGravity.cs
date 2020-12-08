using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnalGravity : MonoBehaviour {
	public FieldCell Space;
	
	void Update() {
		if(Space.Content == null){
			if(Space.Top != null){
				if(Space.Top.Content != null){
					Space.Swap(Space.Top);
				}
			}
		}
	}
}
