using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody myRB;
	public float moveSpeed;
	
	public PlayerControl thePlayer;
	
	
    void Start()
    {
		myRB = GetComponent<Rigidbody>();
		thePlayer = FindObjectOfType<PlayerControl>();
	
    }
	
	void FixedUpdate(){
		
		myRB.AddForce(transform.forward * moveSpeed);
	}

    
    void Update()
    {
        transform.LookAt(thePlayer.transform.position);
    }
}
