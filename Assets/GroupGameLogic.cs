using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupGameLogic : MonoBehaviour {
	public Field Table;
	int mouseX = 0;
	int mouseY = 0;
	bool mousePressed = false;
	
	void Update(){
		if(Input.GetMouseButtonDown(0)){
			mousePressed = true;
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseX = Table.ScreenToLogicX(mousePos);
			mouseY = Table.ScreenToLogicY(mousePos);
		}
		if(Input.GetMouseButtonUp(0)){
			if(mousePressed){
				Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouseX = Table.ScreenToLogicX(mousePos);
				mouseY = Table.ScreenToLogicY(mousePos);
				TryRemoveGroup(mouseX, mouseY);
			}
			mousePressed = false;
		}
	}
	
	bool TryRemoveGroup(int x, int y){
		if(!Table.HasBlock(x, y)){
			Debug.Log("has no block");	// DEBUG
			return false;
		}
		bool[,] selection = new bool[Table.Width, Table.Height];
		int num = SelectGroup(x, y, selection);
		if(num < 2)
		{
			Debug.Log("num < 2");	// DEBUG
			return false;
		}
		Debug.Log("start deleting");	// DEBUG
		for(int i = 0; i < Table.Width; i++){
			for(int j = 0; j < Table.Height; j++){
				if(selection[i, j]){
					Table.DestroyBlock(i, j);
				}
			}
		}
		Table.ApplyAllGravity();
		Debug.Log(num);	// DEBUG
		return true;
	}
	
	int SelectGroup(int x, int y, bool[,] selection){
		Queue<int> xs = new Queue<int>();
		Queue<int> ys = new Queue<int>();
		int lookX, lookY, num = 0;
		int max = Table.Width * Table.Height;
		if(Table.HasBlock(x, y)){
			xs.Enqueue(x);
			ys.Enqueue(y);
			while(max > 0 && xs.Count > 0 && ys.Count > 0){
				lookX = xs.Dequeue();
				lookY = ys.Dequeue();
				max--;
				selection[lookX, lookY] = true;
				num++;
				if(Table.HasBlock(lookX, lookY+1))
					if(!selection[lookX, lookY+1] && Table.GetColor(lookX, lookY+1)
					   == Table.GetColor(lookX, lookY))
				{
					xs.Enqueue(lookX);
					ys.Enqueue(lookY+1);
				}
				if(Table.HasBlock(lookX, lookY-1))
					if(!selection[lookX, lookY-1] && Table.GetColor(lookX, lookY-1)
					   == Table.GetColor(lookX, lookY))
				{
					xs.Enqueue(lookX);
					ys.Enqueue(lookY-1);
				}
				if(Table.HasBlock(lookX+1, lookY))
					if(!selection[lookX+1, lookY] && Table.GetColor(lookX+1, lookY)
					   == Table.GetColor(lookX, lookY))
				{
					xs.Enqueue(lookX+1);
					ys.Enqueue(lookY);
				}
				if(Table.HasBlock(lookX-1, lookY))
					if(!selection[lookX-1, lookY] && Table.GetColor(lookX-1, lookY)
					   == Table.GetColor(lookX, lookY))
				{
					xs.Enqueue(lookX-1);
					ys.Enqueue(lookY);
				}
			}
			
		}
		if(max == 0){
			Debug.Log("infinite loop");	// DEBUG
		}
		return num;
	}
	
}
