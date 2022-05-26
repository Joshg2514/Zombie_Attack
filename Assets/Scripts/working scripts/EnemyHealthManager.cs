using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    
	public int health;
	private int currentHealth;
	public GameObject body;
	private Rigidbody rb, bd;
	
    void Start()
    {
        currentHealth = health;
		
    }

    
    void Update()
    {
		if(currentHealth <= 0)
		{
			rb = GetComponent<Rigidbody>();
			Instantiate(body, transform.position, transform.rotation);
			bd = body.GetComponent<Rigidbody>();
			bd.velocity = rb.velocity;
			Destroy(gameObject);
			
		}
    }
	
	public void HurtEnemy(int damage)
	{
		currentHealth -= damage;
		
	}
}
