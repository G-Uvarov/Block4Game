using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSquareField : MonoBehaviour {
	public Field owner;
	public GameObject[] Prefabs;
	public int Width, Height;
	
	void Start () {
		owner.InitField(Width, Height);
		System.Random rand = new System.Random(12);
		for(int i = 0; i < Width; i++)
			for(int j = 0; j < Height; j++){
			owner.FreeSpace(i, j);
			int num = rand.Next(0, Prefabs.Length);
			GameObject b = Instantiate(Prefabs[num], owner.LogicToScreenPoint(i, j), Quaternion.identity);
			owner.PutBlock(i, j, b.GetComponent<Block>());
		}
		
	}
	
}
