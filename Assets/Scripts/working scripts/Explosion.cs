using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public float power, radius, upForce;
	public GameObject bomb;
	public GameObject bigExplosionPrefab;
	public int damage;
	public bool timeBomb;
	
	
	
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
		
       
    }
	public void Dentonate()
	{
		Instantiate(bigExplosionPrefab, transform.position, transform.rotation);
		Vector3 explosionPosition = bomb.transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
		
		DentonateDmg();
		foreach(Collider hit in colliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if(rb != null)
			{
				rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
			}
		}
		/*
		Vector3 explosionPosition_2 = bomb.transform.position;
		Collider[] colliders_2 = Physics.OverlapSphere(explosionPosition_2, radius);
		foreach(Collider hit in colliders_2)
		{
			Rigidbody rb_2 = hit.GetComponent<Rigidbody>();
			if(rb_2 != null)
			{
				rb_2.AddExplosionForce(power, explosionPosition_2, radius, upForce, ForceMode.Impulse);
			}
		}
		*/
		
		 
	}	
	public void DentonateDmg(){
		
		Instantiate(bigExplosionPrefab, transform.position, transform.rotation);
		Vector3 explosionPosition = bomb.transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
		foreach(Collider hit in colliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if(hit.gameObject.tag == "Enemy")
				rb.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);	
		}
	}
}
