using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Maze : MonoBehaviour {
	public int Size = 1;
	public class MazeData{
		public MazeCell[,] MazeArray;
		public int MazeSize;
		public int[] MazeStart;
		public int[] MazeEnd;
		public int[] CurrentPos;
		public Stack CellSta;

		public MazeData(int Size){
			MazeSize = Size;
			MazeArray = new MazeCell[MazeSize,MazeSize];
			for(int i = 0; i < MazeSize;i++){
				for(int j = 0; j < MazeSize; j++){
					int[] celPos = {i,j};
					MazeArray[i,j] = new MazeCell(celPos);
				}
			}
			CellSta = new Stack();
			MazeStart = new int[2];
			MazeEnd = new int[2];
			CurrentPos = new int[2];
			DepthCreate();
		}

		public void DepthCreate(){
			MazeStart[0] = Random.Range (0,MazeSize -1);
			MazeStart [1] = MazeSize-1;//Random.Range (0,MazeSize -1);
			MazeEnd[0] = Random.Range (0,MazeSize -1);
			MazeEnd [1] = MazeSize - 1;//Random.Range (0,MazeSize -1);
			/*while(MazeEnd [0] == MazeStart [0] && MazeEnd [1] == MazeStart [1]) {
				MazeEnd[0] = Random.Range (0,1);
				MazeEnd[1] = Random.Range (0,1);
			}*/
			CurrentPos[0] = MazeStart[0];
			CurrentPos[1] = MazeStart[1];
			MazeArray [MazeStart [0], MazeStart [1]].CellVisited = true;

			CellSta.Push (MazeArray [MazeStart [0], MazeStart [1]]);
			while (CellSta.Count > 0) { // add for empty PossibleDir to pop off stack
				int Direction = 0;
				int RevDir = 0;
				int CurDir = 0;
				int[,] PossibleDir = DirCheck ();
				if(PossibleDir.GetLength(0) == 0){
					MazeCell BackTrack = CellSta.Pop() as MazeCell;
					CurrentPos[0] = BackTrack.CellPosition[0];
					CurrentPos[1] = BackTrack.CellPosition[1];
					continue;
				}
				Direction = Random.Range (0, PossibleDir.GetLength(0));
				Debug.Log (Direction);
				CurDir = PossibleDir [Direction, 2];
				MazeArray [CurrentPos [0], CurrentPos [1]].BreakWall (CurDir);
				CurrentPos [0] = PossibleDir [Direction, 0];
				CurrentPos [1] = PossibleDir [Direction, 1];
				CellSta.Push (MazeArray [CurrentPos [0], CurrentPos [1]]);
				switch (CurDir) {
				case 1:
					RevDir = 4;
					break;
				case 4:
					RevDir = 1;
					break;
				case 2:
					RevDir = 3;
					break;
				case 3:
					RevDir = 2;
					break;
				}
				MazeArray [CurrentPos [0], CurrentPos [1]].BreakWall (RevDir);
			}
		}

		private  int[,] DirCheck(){
			int[] Up = {CurrentPos[0]-1,CurrentPos[1]};
			int[] Down = {CurrentPos[0]+1,CurrentPos[1]};
			int[] Left = {CurrentPos[0],CurrentPos[1]-1};
			int[] Right = {CurrentPos[0],CurrentPos[1]+1};
			bool UpBool = false;
			bool DownBool = false;
			bool LeftBool = false;
			bool RightBool = false;
			int size = 0;
			if (Up[0] >= 0 && !(MazeArray[Up [0], Up[1]].CellVisited)) {
				UpBool = true;
				size++;
			}
			if (Down[0] < MazeSize && !(MazeArray[Down [0], Down[1]].CellVisited)) {
				DownBool = true;
				size++;
			}
			if (Left[1] >= 0 && !(MazeArray[Left [0], Left[1]].CellVisited)) {
				LeftBool = true;
				size++;
			}
			if (Right[1] < MazeSize && !(MazeArray[Right [0], Right[1]].CellVisited)) {
				RightBool = true;
				size++;
			}
			if (size == 0) {
				int[,] DirPosERR = new int[0, 0];
				return DirPosERR;
			}
			int[,] DirPos = new int[size, 3];
			size = 0;
			if (UpBool) {
				DirPos[size,0] = Up[0];
				DirPos[size,1] = Up[1];
				DirPos [size, 2] = 1;
				size++;
			}
			if (DownBool) {
				DirPos[size,0] = Down[0];
				DirPos[size,1] = Down[1];
				DirPos [size, 2] = 4;
				size++;
			}
			if (LeftBool) {
				DirPos[size,0] = Left[0];
				DirPos[size,1] = Left[1];
				DirPos [size, 2] = 2;
				size++;
			}
			if (RightBool) {
				DirPos[size,0] = Right[0];
				DirPos[size,1] = Right[1];
				DirPos [size, 2] = 3;
			}
			return DirPos;
		}

	}
	// Use this for initialization
	void Start () {
		GenerateMap(Size);

	}


	// Update is called once per frame
	void Update () {

	}

	void GenerateMap(int Size){// create the floor plan, create floor plane, create wall mesh
		MazeData FloorCreate = new MazeData(Size);
		float FloorScale = Size/10.0f;
		float xTrans = 5.0f*FloorScale;
		float zTrans = 5.0f*FloorScale;
		gameObject.transform.localPosition = new Vector3(xTrans,0,zTrans);
		gameObject.transform.localScale *= FloorScale;
		for(int i = 0; i < Size;i++){
			for(int j = 0; j < Size; j++){
				int[] celPos = {i,j};
				FloorCreate.MazeArray[i,j].GenWall();
			}
		}


	}
}
