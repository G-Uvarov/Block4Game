using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
	Block[,] content;
	bool[,] space;
	int w, h;
	public int Width{get{return w;}}
	public int Height{get{return h;}}
	
	public Vector2 LogicToScreenPoint(int x, int y){
		return new Vector2(x, -y);
	}
	
	public int ScreenToLogicX(Vector2 point){
		return Mathf.RoundToInt(point.x);
	}
	
	public int ScreenToLogicY(Vector2 point){
		return Mathf.RoundToInt(-point.y);
	}
	
	public int GetColor(int x, int y){
		if(HasBlock(x, y)){
			return content[x, y].Color;
		}
		return -1;
	}
	
	public void InitField(int width, int height){
		if(width < 0) width = 0;
		if(height < 0) height = 0;
		w = width;
		h = height;
		content = new Block[w, h];
		space = new bool[w, h];
	}
	
	public bool FreeSpace(int x, int y){
		if(x < 0 || x >= w || y < 0 || y >= h){
			return false;
		}
		space[x, y] = true;
		return true;
	}
	
	public bool RemoveSpace(int x, int y){
		if(x < 0 || x >= w || y < 0 || y >= h){
			return false;
		}
		DestroyBlock(x, y);
		space[x, y] = false;
		return true;
	}
	
	public bool HasSpace(int x, int y){
		if(x < 0 || x >= w || y < 0 || y >= h){
			return false;
		}else{
			return space[x,y] && !content[x,y];
		}
	}
	
	public bool HasBlock(int x, int y){
		if(x < 0 || x >= w || y < 0 || y >= h){
			return false;
		}else{
			return space[x,y] && content[x,y];
		}
	}
	
	public bool MoveBlock(int xorig, int yorig, int x, int y){
		if(!HasSpace(x, y) || !HasBlock(xorig, yorig)){
			return false;
		}
		content[x, y] = content[xorig, yorig];
		content[xorig, yorig] = null;
		content[x, y].X = x;
		content[x, y].Y = y;
		
		return true;
	}
	
	public bool SwapBlocks(int xorig, int yorig, int x, int y){
		if(!HasBlock(x, y) || !HasBlock(xorig, yorig)){
			return false;
		}
		Block swap = content[x, y];
		content[x, y] = content[xorig, yorig];
		content[xorig, yorig] = swap;
		content[x, y].X = x;
		content[x, y].Y = y;
		content[xorig, yorig].X = xorig;
		content[xorig, yorig].Y = yorig;
		
		return true;
	}
	
	public bool DestroyBlock(int x, int y){
		if(!HasBlock(x, y)){
			return false;
		}
		content[x, y].Destroyed = true;
		content[x, y] = null;
		return true;
	}
	
	
	public bool PutBlock(int x, int y, Block block){
		if(!HasSpace(x, y) || !block){
			return false;
		}
		content[x, y] = block;
		block.X = x;
		block.Y = y;
		block.Owner = this;
		return true;
	}
	
	int ApplyStraightGravity(){
		int num = 0;
		for(int x = 0; x < w; x++){
			for(int y = h-1; y >= 0; y--){
				if(HasBlock(x, y) && HasSpace(x,y+1)){
					MoveBlock(x,y,x,y+1);
					num++;
				}
			}
		}
		return num;
	}
	
	int ApplyLeftGravity(){
		int num = 0;
		for(int x = 0; x < w; x++){
			for(int y = h-1; y >= 0; y--){
				if(HasBlock(x, y) && HasSpace(x-1,y+1)){
					MoveBlock(x,y,x-1,y+1);
					num++;
				}
			}
		}
		return num;
	}
	
	
	int ApplyRightGravity(){
		int num = 0;
		for(int x = 0; x < w; x++){
			for(int y = h-1; y >= 0; y--){
				if(HasBlock(x, y) && HasSpace(x+1,y+1)){
					MoveBlock(x,y,x+1,y+1);
					num++;
				}
			}
		}
		return num;
	}
	
	public void ApplyAllGravity(){
		int max = h*w;
		int g = 1;
		while(g > 0 && max > 0){
			g = ApplyStraightGravity();
			max--;
			// spawn at top
		}
		g = 1;
		while(g > 0 && max > 0){
			max--;
			g = ApplyLeftGravity();
			// spawn at top
			g += ApplyRightGravity();
			//spawn at top
		}
		if(max <= 0){
			Debug.Log("ινφινιτε λοοπ");	// DEBUG
		}
		
	}
	
	
}
