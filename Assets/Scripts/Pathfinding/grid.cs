using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utilities;

public class Grid<TGridObject> { //<T> means this class will now accept generics
	
	public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
	public class OnGridObjectChangedEventArgs : EventArgs {
		public int x;
		public int y;
	}
	
	private int width;
	private int height;
	private float cellSize;
	private Vector3 originPosition;
	//private int[,] gridArray;
	private TextMesh[,] debugTextArray;
	private TGridObject[,] gridArray; // custom type
	

	
	public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject) {
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;
		this.originPosition = originPosition;
		
		
		
		gridArray = new TGridObject[width, height]; //array used to pass values to CreateWorldText constructor, now a a generic type TGridObject
		
		
		bool showDebug = true;
		
		if (showDebug){
		debugTextArray = new TextMesh[width, height]; //array of instantiated TextMesh created by CreateWorldText, used to manipulate a particular element	
		
		//Debug.Log(width + " " + height);
		
		for (int x = 0; x < gridArray.GetLength(0); x++) {
			for (int y = 0; y < gridArray.GetLength(1); y++){
					gridArray[x,y] = createGridObject(this, x, y);
			}
		}
		
		
		for (int x=0; x<gridArray.GetLength(0); x++){
			for (int y=0; y<gridArray.GetLength(1); y++) {
				//Debug.Log(x + "," + y);
				//debugTextArray[x, y] = utilities.CreateWorldText(gridArray[x,y]?.ToString(), null,
				//	GetWorldPosition(x, y) + new Vector3(cellSize*.75f, cellSize) * .6f, 4,
				//	Color.white, TextAnchor.MiddleCenter);

				
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 300f);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 300f);
			}
		}	
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 300f);
		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 300f);		
		
		OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
			//debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
		};
		}
	}
	
	public Vector3 GetWorldPosition(int x, int y) {
		 Quaternion newRotation = Quaternion.Euler(90,0,0);
		return newRotation * new Vector3(x,y) * cellSize + originPosition;
	}
	
	public void GetXY(Vector3 worldPosition, out int x, out int y) { // set to void to return multiple values from this one function, I could also rewrite the code to use a struct for X and Y but this works too
		x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
		y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
	}
		
	public void SetGridObject(int x, int y, TGridObject value){ // sets the value for a specified element of the TextMesh array
		if (x >= 0 && y >= 0 && x < width && y < height){
			gridArray[x, y] = value;
			if(OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y});
			//debugTextArray[x, y].text = gridArray[x, y].ToString();		
		}
	}
	
	public void SetGridObject(Vector3 worldPosition, TGridObject value){ //sets the value for a specified element based on the world position of the element
		int x, y;
		GetXY(worldPosition, out x, out y);
		SetGridObject(x, y, value);
	}
	
	public TGridObject GetGridObject(int x, int y){ // gets the value for a specified element of the TextMesh array
		if (x >= 0 && y >= 0 && x < width && y < height){
			//Debug.Log(x + " & " + gridArray[x, y]);
			return gridArray[x, y];
		}
		else{
			//Debug.Log("0");
		return default(TGridObject);
		}
	}
	
	public TGridObject GetGridObject(Vector3 worldPosition){ // gets the value for a specified element of the TextMesh array
		int x, y;
		GetXY(worldPosition, out x, out y);
		//Debug.Log(x + " " + y);
		return GetGridObject(x, y);
	}
	
	public void TriggerGridObjectChanged(int x, int y) {
		if(OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y});
	}
	
	public int GetWidth(){
		return width;
	}
	
	public int GetHeight(){
		return height;
	}
	
}
