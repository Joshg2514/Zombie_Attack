using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding 
{ 

	private Grid<PathNode> grid;
	private HashSet<PathNode> closedList;
	private List<PathNode> openList;
	
	
	private  const int STRAIGHT_MOVE_COST = 10; 
	private  const int DIAGONAL_MOVE_COST = 14;
	
	public static Pathfinding Instance { get; private set;}
	
	public Pathfinding(int width, int height) 
	{
		Instance = this;
		grid = new Grid<PathNode>(width, height, 1f, new Vector3(0, 0), (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
		
	}
	public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition) {
		grid.GetXY(startWorldPosition, out int startX, out int startY);
		grid.GetXY(endWorldPosition, out int endX, out int endY);
		
		List<PathNode> path = FindPath(startX, startY, endX, endY);
		if(path == null) {
			return null;
		} else {
			List<Vector3> vectorPath = new List<Vector3>();
			foreach(PathNode pathNode in path) {
				vectorPath.Add(grid.GetWorldPosition(pathNode.x, pathNode.y) * 1f + Vector3.one * 1f * .5f);
			//	Debug.DrawLine(grid.GetWorldPosition(pathNode.x, pathNode.y) * 1f + Vector3.one * 1f * .5f,GetPosition(),  Color.green, 1f);
			}
			return vectorPath;
	}
	}
	public List<PathNode> FindPath(int startX, int startY, int endX, int endY) 
	{
		/*
			A Star can be broken down into steps:
			1 - add start Node to the open list, initialize the nodes of the grid
			2 - find values of start Node (G, H, F)
			3 - run through open list, find the lowest f cost in the list (pass the open list to another method that will return the lowest f node)
			4 - is this node the goal? Return this node! add to closed list if not (remove from open list)
			5 - find the neighbours of that node
			6 - add to open list if they are not already in it/closed list
			7 - calculate the values of those nodes (G, H, F)
			8 - if the are already on the open list, if the new values are lower that the previous, update them with the new value. Also update the prev node
			9 - repeat 3-8 until goal is found
			10 - once goal is found, calculate the path back to the start by following the previous node values (using CalculatePath function)
		*/
		
		//Step 1
		PathNode startNode = grid.GetGridObject(startX, startY);
		PathNode endNode = grid.GetGridObject(endX, endY);
		
		openList = new List<PathNode> {startNode};
		closedList = new HashSet<PathNode>();
		
		for (int x = 0; x < grid.GetWidth(); x++){
			for (int y = 0; y < grid.GetHeight(); y++) {
				
				PathNode pathNode = grid.GetGridObject(x, y);
				if(pathNode.blocked) closedList.Add(pathNode);
				pathNode.gCost = int.MaxValue;
				pathNode.CalculateFCost();
				grid.TriggerGridObjectChanged(pathNode.x, pathNode.y);
				pathNode.prevNode = null;
				
			}
		}
		
		//Step 2
		startNode.gCost = 0;
		startNode.hCost = CalculateDistance(startNode, endNode);
		startNode.CalculateFCost();
		
		//Step 3
		while (openList.Count > 0){
			PathNode currentNode = GetLowestFCost(openList);
			
			//Step 4
			if(currentNode == endNode) {
				//END NODE FOUND
				return CalculatePath(endNode);
			}
			
			openList.Remove(currentNode);
			closedList.Add(currentNode);
			
			
			
			
			foreach(PathNode neighbourNode in GetNeighbourList(currentNode)){
				if(closedList.Contains(neighbourNode)) continue;
				
				int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode);
				if(tentativeGCost < neighbourNode.gCost){
					neighbourNode.prevNode = currentNode;
					neighbourNode.gCost = tentativeGCost;
					neighbourNode.hCost = CalculateDistance(neighbourNode, endNode);
					neighbourNode.CalculateFCost();
					grid.TriggerGridObjectChanged(neighbourNode.x, neighbourNode.y);
					if(!openList.Contains(neighbourNode)){
						openList.Add(neighbourNode);
					}
				}

			}				
		}
		
		return null;
		
		
		
	}		
	
	private int CalculateDistance(PathNode a, PathNode b) 
	{
		int xDistance = Mathf.Abs(a.x - b.x);
		int yDistance = Mathf.Abs(a.y - b.y);
		int remaining = Mathf.Abs(xDistance - yDistance);
		return (DIAGONAL_MOVE_COST * Mathf.Min(xDistance , yDistance) + STRAIGHT_MOVE_COST * remaining);
	}
	
	private PathNode GetLowestFCost(List<PathNode> pathNodeList)
	{
		PathNode pathNodeLowest = pathNodeList[0];
		foreach(PathNode pathNode in pathNodeList){
			if(pathNode.fCost < pathNodeLowest.fCost){
				pathNodeLowest = pathNode;
			}
		}
		return pathNodeLowest;
	}		
	
	private List<PathNode> CalculatePath (PathNode currentNode)
	{
		List<PathNode> pathList = new List<PathNode>();
		
		while (currentNode.prevNode != null){
			pathList.Add(currentNode);
			currentNode = currentNode.prevNode;
		}
		
		pathList.Add(currentNode);
		
		pathList.Reverse();
		return pathList;
	}
	
	private List<PathNode> GetNeighbourList(PathNode currentNode) 
	{
		List<PathNode> neighbourList = new List<PathNode>();
		
		
		if(currentNode.x - 1 >= 0){
			// Left
			neighbourList.Add(grid.GetGridObject(currentNode.x - 1, currentNode.y));
			// Left Up
			if(currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(grid.GetGridObject(currentNode.x - 1, currentNode.y + 1));
			// Left Down
			if(currentNode.y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.x - 1, currentNode.y - 1));
		}
		if(currentNode.x + 1 < grid.GetWidth()){
			// Right
			neighbourList.Add(grid.GetGridObject(currentNode.x + 1, currentNode.y));
			// Right Up
			if(currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(grid.GetGridObject(currentNode.x + 1, currentNode.y + 1));
			// Right Down
			if(currentNode.y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.x + 1, currentNode.y - 1));
		}
		// Down 
		if(currentNode.y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.x, currentNode.y - 1));
		// Up
		if(currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(grid.GetGridObject(currentNode.x, currentNode.y + 1));
		
		return neighbourList;
		
	}
	
	public Grid<PathNode> GetGrid() {
		return grid;
	}
	
}

