using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete2Group : MonoBehaviour {
	public FieldClickHandler Target;
	public int MinGroup;
	
	void Start(){
		if(Target != null){
			Target.OnClick.AddListener(OnClick);
		}
	}
	
	void OnClick(FieldCell cell){
		HashSet<FieldCell> selection = SelectColoredGroup(cell);
		if(selection.Count >= MinGroup){
			foreach(FieldCell i in selection){
				i.DestroyInside();
				Debug.LogFormat("Destroyed {0}", selection.Count);
			}
		}
	}
	
	HashSet<FieldCell> SelectColoredGroup(FieldCell origin){
		HashSet<FieldCell> r = new HashSet<FieldCell>();
		Queue<FieldCell> q = new Queue<FieldCell>();
		if(origin.Content is ColoredBlock)
			q.Enqueue(origin);
		while((q.Count > 0)){
			FieldCell current = q.Dequeue();
			if(!r.Contains(current))
				r.Add(current);
			FieldCell[] s = current.Siblings;
			foreach(FieldCell i in s){
				if(!r.Contains(i) && i.Content is ColoredBlock){
					ColoredBlock c1, c2;
					c1 = current.Content as ColoredBlock;
					c2 = i.Content as ColoredBlock;
					if(c1.Color == c2.Color){
						q.Enqueue(i);
					}
				}
			}
		}
		return r;
	}
}
