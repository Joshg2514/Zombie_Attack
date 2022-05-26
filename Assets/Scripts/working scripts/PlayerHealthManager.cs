using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int startingHealth;
	private int currentHealth;
	
	public float flashLength;
	private float flashCounter;
	
	private Renderer rend;
	private Color storedColor;
	
    void Start()
    {
		currentHealth = startingHealth;
		rend = GetComponent<Renderer>();
		storedColor = rend.material.GetColor("_Color");
    }

    
    void Update()
	{
		if(currentHealth <= 0)
		{
			gameObject.SetActive(false);
		}
		
		if(flashCounter > 0)
		{
			flashCounter -= Time.deltaTime;
			if(flashCounter	<= 0)
			{
				rend.material.SetColor("_Color", storedColor);
			}
		}
	}
	public void HurtPlayer(int damageAmount)
	{
		currentHealth -= damageAmount;
		
		flashCounter = flashLength;
		rend.material.SetColor("_Color", Color.white);
	}
}
