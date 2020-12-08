using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredBlock : MonoBehaviour, IFieldObject{
	public int Color;
	public Vector2 Place {get;set;}
	public bool Destroyed {get; set;}
	
	void Update(){
		if(Destroyed){
			Destroy(gameObject);
		}
	}
}
