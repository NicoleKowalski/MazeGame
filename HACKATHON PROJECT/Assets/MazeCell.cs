using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

	public int[] CellPosition;
	public bool[,] Cell;
	public bool CellVisited;
	public List<GameObject> WallList;

	public MazeCell(int[] celPos){
		Cell = new bool[2,2];
		Cell[0,0] = true;
		Cell[0,1] = true;
		Cell[1,0] = true;
		Cell[1,1] = true;
		CellVisited = false;
		CellPosition = celPos;
	}

	public void BreakWall(int Side){ // U:1,D:4,L:2,R:3
		CellVisited = true;
		switch (Side)
		{
		case 1:
			Cell [0, 0] = false; 
			break;
		case 4:
			Cell [1, 1] = false;
			break;
		case 2:
			Cell [1, 0] = false;
			break;
		case 3:
			Cell [0, 1] = false;
			break;
		}
	}
	public void GenWall(){
		if (Cell [0, 0]) {
			float xPos = CellPosition [0];
			float zPos = CellPosition [1]+0.45f;
			GameObject temp = Instantiate(Resources.Load("WallPrefab"),new Vector3(xPos,0.85f, zPos),Quaternion.Euler(0,0,90)) as GameObject;
		}
		if (Cell [1, 1]) {
			float xPos = CellPosition [0]+1.0f;
			float zPos = CellPosition [1] + 0.45f;
			GameObject temp =  (Instantiate(Resources.Load("WallPrefab"),new Vector3(xPos,0.85f, zPos),Quaternion.Euler(0,180,90)) as GameObject);
					}
		if (Cell [1, 0]) {
			float xPos = CellPosition [0]+0.45f;
			float zPos = CellPosition [1];
			GameObject temp =  (Instantiate(Resources.Load("WallPrefab"),new Vector3(xPos,0.85f, zPos),Quaternion.Euler(0,90,90)) as GameObject);
					}
		if (Cell [0, 1]) {
			float xPos = CellPosition [0]+0.45f;
			float zPos = CellPosition [1]+1.0f;
			GameObject temp = (Instantiate(Resources.Load("WallPrefab"),new Vector3(xPos,0.85f, zPos),Quaternion.Euler(0,270,90)) as GameObject);
		}
	}

}
