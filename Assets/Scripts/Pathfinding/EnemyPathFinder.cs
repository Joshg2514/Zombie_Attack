using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinder : MonoBehaviour
{
	private const float speed = 40f;
    private int currentPathIndex;
	private List<Vector3> pathVectorList;
	
    void Start()
    {
		Transform bodyTransform = transform.Find("Body");
        
    }

    // Update is called once per frame
    void Update()
    {
		HandleMovement();
        
    }
	private void HandleMovement() {
		if(pathVectorList != null){
			Vector3 targetPosition = pathVectorList[currentPathIndex];
			if(Vector3.Distance(transform.position, targetPosition) > 1f) {
				Quaternion newRotation = Quaternion.Euler(0,0,0);
				Vector3 moveDir = (targetPosition - transform.position).normalized;
				
				float distanceBefore = Vector3.Distance(transform.position, targetPosition);
				transform.position = transform.position + (newRotation *moveDir) * speed * Time.deltaTime;
				
				transform.LookAt(targetPosition);
			}else {
				currentPathIndex++;
				if(currentPathIndex >= pathVectorList.Count) {
					StopMoving();
				}
			}
		}
	}
	private void StopMoving() {
		pathVectorList = null;
	}
	
	public Vector3 GetPosition() {
		return transform.position;
	}
	
	public void SetTargetPosition(Vector3 targetPosition) {
		currentPathIndex = 0;
		Quaternion newRotation = Quaternion.Euler(0,0,0);
		pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), newRotation * targetPosition+ Vector3.one *.5f);
		
		if(pathVectorList != null){
				
				//Debug.Log("checking");
				//Debug.Log(path[startX].x + "," + path[startY].y);
				//Debug.Log("checking..");
				for(int i = 0; i < pathVectorList.Count - 1; i++){
				Debug.Log(pathVectorList[i+1].x + "," + pathVectorList[i+1].y);
				Pathfinding.Instance.GetGrid().GetXY(pathVectorList[i], out int x1, out int y1);
				Pathfinding.Instance.GetGrid().GetXY(pathVectorList[i+1], out int x2, out int y2);
				
				Debug.DrawLine(Pathfinding.Instance.GetGrid().GetWorldPosition(x1, y1) + Vector3.one *.5f, Pathfinding.Instance.GetGrid().GetWorldPosition(x2, y2)+ Vector3.one *.5f, Color.green, 1f);
				//grid.TriggerGridObjectChanged(x, y);
				}
		
		}
		
		
		if(pathVectorList != null && pathVectorList.Count > 1) {
			pathVectorList.RemoveAt(0);
		}
	}
}
