using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour{
	public int Color;
	public bool Destroyed = false;
	public int X, Y;
	public Field Owner;
	public float Speed;
	
	void Update(){
		Vector2 target = Owner.LogicToScreenPoint(X, Y);
		if(Vector2.Distance(transform.position, target) < Speed * Time.deltaTime * 2){
			transform.position = target;
		}else{
			Vector2 dir = target - (Vector2)transform.position;
			transform.position += (Vector3)(dir.normalized * Speed * Time.deltaTime);
		}
		
		if(Destroyed)
			Destroy(gameObject);
	}
}
