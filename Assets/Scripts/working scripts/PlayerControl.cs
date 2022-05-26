using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
	private Rigidbody myRigidbody;
	
	private Vector3 moveInput;
	private Vector3 moveVelocity;
	KeyCode fire;
	private Camera mainCamera;
	
	public GunController theGun;
	
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput * moveSpeed;
		
		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength;
		
		if(groundPlane.Raycast(cameraRay, out rayLength))
		{
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
			
			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}
		
		//Shooting
		if(theGun.allowButtonHold) 
			theGun.isFiring = Input.GetKey(KeyCode.Mouse0);
		else 
			theGun.isFiring = Input.GetKeyDown(KeyCode.Mouse0);
		//Reloading
		if(Input.GetKeyDown(KeyCode.R) && theGun.bulletsLeft < theGun.magSize && !theGun.reloading) 
			theGun.Reload();
		
		
		//if(Input.GetMouseButtonDown(0)){
		//	theGun.isFiring = true;
		//}
		//if(Input.GetMouseButtonUp(0)){
		//	theGun.isFiring = false;
		//}
    }	
	
	void FixedUpdate () {
		myRigidbody.velocity = moveVelocity;
	}
	
}

