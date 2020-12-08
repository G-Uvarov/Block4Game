using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramidalGravity : MonoBehaviour {
	public FieldCell Space;
	
	void Update() {
		if(Space.Content == null){
			bool col = false;
			if(Space.Top != null){
				if(Space.Top.Content != null){
					col = Space.Swap(Space.Top);
				}
			}
			if(!col){
				FieldCell rTop = null, lTop = null;
				if(Space.Top != null){
					rTop = Space.Top.Right;
					lTop = Space.Top.Left;
				}else{
					if(Space.Right != null)
						rTop = Space.Right.Top;
					if(Space.Left != null)
						lTop = Space.Left.Top;
				}
				FieldCell first = rTop, second = lTop;
				if((Space.RoundedX + Space.RoundedY) % 2 == 0){
					first = lTop;
					second = rTop;
				}
				if(first != null && first.Content != null){
					col = Space.Swap(first);
				}
				if(!col && second != null && second.Content != null){
					col = Space.Swap(first);
				}
			}
		}
	}
	
}
