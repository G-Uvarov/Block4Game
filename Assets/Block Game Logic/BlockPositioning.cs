using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPositioning : MonoBehaviour {
	IFieldObject Target;
	public float Speed;
	
	void Start(){
		Target = GetComponent<IFieldObject>();
	}
	
	void Update(){
		if(!Target.Destroyed){
			Vector3 dir = (Target.Place - (Vector2)transform.position).normalized;
			if(Vector2.Distance(transform.position, Target.Place) < Speed * Time.deltaTime){
				transform.position = Target.Place;
			}else{
				transform.position += dir * Speed * Time.deltaTime;
			}
			
		}
	}
}
