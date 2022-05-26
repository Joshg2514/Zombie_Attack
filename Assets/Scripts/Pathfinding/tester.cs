using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static utilities;


public class tester : MonoBehaviour
{
	
	[SerializeField] private  EnemyPathFinder enemyPathFinder;
	private int startX;
	private int startY;
	
	public GameObject bigExplosionPrefab;
	
    public Pathfinding pathfinding;
	
	private Camera mainCamera;
	
	private void Start() {
		 pathfinding = new Pathfinding(45, 30);
		 mainCamera = FindObjectOfType<Camera>();
		 
	}
	
	private void Update() {
		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.back, Vector3.left, Vector3.forward);
		float rayLength;
		
		if(groundPlane.Raycast(cameraRay, out rayLength))
		{
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
			
		}
		
		if (Input.GetMouseButtonDown(0)) {
			//pathfinding.Clicked();
			Vector3 position = utilities.GetMouseWorldPosition();
			pathfinding.GetGrid().GetXY(cameraRay.GetPoint(rayLength), out int x, out int y);
			List<PathNode> path = pathfinding.FindPath(startX, startY, x, y);
			
			if(path != null){
				
				Debug.Log("checking");
				//Debug.Log(path[startX].x + "," + path[startY].y);
				Debug.Log("checking..");
				for(int i = 0; i < path.Count - 1; i++){
				Debug.Log(path[i+1].x + "," + path[i+1].y);
				//Debug.DrawLine(pathfinding.GetGrid().GetWorldPosition(path[i].x, path[i].y) + Vector3.one *.5f, pathfinding.GetGrid().GetWorldPosition(path[i+1].x, path[i+1].y)+ Vector3.one *.5f, Color.green, 1f);
				//grid.TriggerGridObjectChanged(x, y);
				}
				
			}
			//pathfinding.GetGrid().GetGridObject(x, y).AddValue(1);
			//pathfinding.GetGrid().GetGridObject(0, 0).AddValue(5);
			//if (pathnode != null) {
			//	pathnode.AddValue(1);
			enemyPathFinder.SetTargetPosition(cameraRay.GetPoint(rayLength));
			}
			//grid.SetGridObject(utilities.GetMouseWorldPosition(), !grid.GetGridObject(utilities.GetMouseWorldPosition()));
		
		
		if (Input.GetMouseButtonDown(1)) {
			Vector3 position = utilities.GetMouseWorldPosition();
			pathfinding.GetGrid().GetXY(cameraRay.GetPoint(rayLength), out int x, out int y);
			
			Debug.Log(x + "," + y + " is new start") ;
			startX = x;
			startY = y;
		//	Debug.Log(grid.GetGridObject(utilities.GetMouseWorldPosition()));
		}
		
		if (Input.GetKeyDown("space")) {
			Vector3 position = utilities.GetMouseWorldPosition();
			pathfinding.GetGrid().GetXY(cameraRay.GetPoint(rayLength), out int x, out int y);
			
			Quaternion newRotation = Quaternion.Euler(0,0,0);
			Instantiate(bigExplosionPrefab, pathfinding.GetGrid().GetWorldPosition(x ,y)* 1f + Vector3.one * 1f * .5f, newRotation);
			
			pathfinding.GetGrid().GetGridObject(x, y).Block();
			Debug.Log(pathfinding.GetGrid().GetGridObject(x, y).blocked);
			pathfinding.GetGrid().TriggerGridObjectChanged(x, y);
		//	Debug.Log(grid.GetGridObject(utilities.GetMouseWorldPosition()));
		}
		
		if (Input.GetKeyDown("up")) {
			Vector3 position = utilities.GetMouseWorldPosition();
			pathfinding.GetGrid().GetXY(cameraRay.GetPoint(rayLength), out int x, out int y);
			pathfinding.GetGrid().GetGridObject(x, y).AddTCost(50);
			Debug.Log(pathfinding.GetGrid().GetGridObject(x, y).tCost);
			//pathfinding.GetGrid().TriggerGridObjectChanged(x, y);
		//	Debug.Log(grid.GetGridObject(utilities.GetMouseWorldPosition()));
		}
		
	}
}
