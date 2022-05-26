using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;
	
	public BulletController bullet;
	
	//Gun stats
	public float bulletSpeed, timeBetweenShots,spread, range, reloadTime;
	public int magSize, bulletsPerShot, damage;
	public bool allowButtonHold;
	public int bulletsLeft, bulletsShot;
	
	public bool readyToShoot, reloading;
	private float shotCounter;
	
	public Transform firePoint;
	
    void Start()
    {
		bulletsLeft = magSize;
		readyToShoot = true;	
    }

    
    void Update()
    {
        if(isFiring)
		{
			if(readyToShoot)
			{
				//fix this stuff my guy
				shotCounter -= Time.deltaTime;
				if(shotCounter <= 0)
					{
						while(bulletsPerShot > bulletsShot)
						{
							float randomNumberY = Random.Range(-spread, spread);
					
							shotCounter = timeBetweenShots;
							BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);// as BulletController;
							newBullet.transform.Rotate(0, randomNumberY, 0);
					
							newBullet.speed = bulletSpeed;
							newBullet.damageToGive = damage;
							bulletsShot++;
						}
					bulletsShot = 0;	
					bulletsLeft--;
					if(bulletsLeft <= 0)
						readyToShoot = false;
					}
			}
			else 
				Reload();
		} 
		else 
			shotCounter = 0;
		
    }
	
	public void Reload()
	{
		reloading = true;
        Invoke("ReloadFinished", reloadTime);
	}
	private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
		readyToShoot = true;
    }
}
