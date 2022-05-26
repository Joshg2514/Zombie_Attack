using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float speed;
	public bool pierce;
	public int damageToGive;
	public bool explosive;
	public Explosion bomb;
	public bool timeBomb;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		if(timeBomb){
			bomb.Dentonate();
			;
		}
		
    }
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
			
			if(explosive)
				bomb.Dentonate();
			if(!pierce)
				Destroy(gameObject);
				
		}
			if(explosive)
				bomb.Dentonate();
			if(!pierce)
				Destroy(gameObject);
	}
}
