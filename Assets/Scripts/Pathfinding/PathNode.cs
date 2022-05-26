using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
	private Grid<PathNode> grid;
	public int x;
	public int y;
	public bool blocked;
	
	public int gCost;
	public int hCost;
	public int fCost;
	public int tCost;
	
	private int g;
	
	public PathNode prevNode;
	
	public PathNode(Grid<PathNode> grid, int x, int y) {
		this.grid = grid;
		this.x = x;
		this.y = y;
		this.blocked = false;
	}
	
	public override string ToString() {
		return (" " + !(blocked));
	}
	
	public void AddValue(int addValue) {
		g += addValue;
		grid.TriggerGridObjectChanged(x, y);
	}
	public void Block(){
		blocked = !(blocked);
	}
	
	public void CalculateFCost() {
		fCost = gCost + hCost + tCost;
	}
	
	public void AddTCost(int a) {
		tCost += a;
	}
  
}
