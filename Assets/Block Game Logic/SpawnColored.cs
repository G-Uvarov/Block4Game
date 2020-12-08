using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColored : MonoBehaviour {
	public ColoredBlock[] Prefabs;
	public FieldCell Cell;
	
	void Update() {
		if(Cell.Content == null){
			Spawn();
		}
	}
	
	void Spawn(){
		if(Cell != null && Cell.Content == null){
			int rand = Random.Range(0, Prefabs.Length);
			GameObject obj = Instantiate(Prefabs[rand].gameObject,
			                               Cell.transform.position,
			                               Quaternion.identity);
			Cell.CreateInside(obj.GetComponent<IFieldObject>());
		}
		
	}
}
