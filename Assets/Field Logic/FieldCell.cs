using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FieldCellEvent : UnityEvent<FieldCell, IFieldObject> {}

public class FieldCell : MonoBehaviour {
	private FieldCell top, left, right, bottom;
	private IFieldObject content;
	public FieldCellEvent
	OnContentDestroy,
	OnContentChange,
	OnContentCreate;
	
	void Start(){
		FieldCell[] potentialSiblings = FindObjectsOfType(typeof(FieldCell)) as FieldCell[];
		foreach(FieldCell i in potentialSiblings){
			if(i){
				if(i.RoundedX == RoundedX && i.RoundedY == RoundedY-1){
					bottom = i;
				}
				if(i.RoundedX == RoundedX && i.RoundedY == RoundedY+1){
					top = i;
				}
				if(i.RoundedX == RoundedX+1 && i.RoundedY == RoundedY){
					right = i;
				}
				if(i.RoundedX == RoundedX-1 && i.RoundedY == RoundedY){
					left = i;
				}
			}
		}
	}
	
	public FieldCell Top{
		get{return top;}
	}
	
	public FieldCell Bottom{
		get{return bottom;}
	}
	
	public FieldCell Right{
		get{return right;}
	}
	
	public FieldCell Left{
		get{return left;}
	}
	
	public IFieldObject Content{
		get{return content;}
	}
	
	private void adjustContentPosition(){
		if(content != null){
			content.Place = new Vector2(RoundedX, RoundedY);
		}
	}
	
	public bool CreateInside(IFieldObject obj){// potential bug if pass an already positioned object
		if(content  != null)
			return false;
		content = obj;
		OnContentCreate.Invoke(this, content);
		OnContentChange.Invoke(this, content);
		adjustContentPosition();
		return true;
	}
	
	public bool DestroyInside(){
		if(content == null)
			return false;
		content.Destroyed = true;
		IFieldObject d = content;
		content = null;
		OnContentDestroy.Invoke(this, d);
		OnContentChange.Invoke(this, content);
		return true;
	}
	
	public bool Swap(FieldCell other){
		if(other == null)
			return false;
		IFieldObject swap = content;
		content = other.content;
		other.content = swap;
		adjustContentPosition();
		other.adjustContentPosition();
		OnContentChange.Invoke(this, content);
		OnContentChange.Invoke(other, other.content);
		return true;
	}
	
	public FieldCell[] Siblings{
		get{
		int num = 0;
		if(top) num++;
		if(bottom) num++;
		if(right) num++;
		if(left) num++;
		FieldCell[] r = new FieldCell[num];
		int i = 0;
		if(top) r[i++] = top;
		if(bottom) r[i++] = bottom;
		if(right) r[i++] = right;
		if(left) r[i++] = left;
		return r;
		}
	}
	
	public int RoundedX{
		get{
			return Mathf.RoundToInt(transform.position.x);
		}
	}
	public int RoundedY{
		get{
			return Mathf.RoundToInt(transform.position.y);
		}
	}
	
}
