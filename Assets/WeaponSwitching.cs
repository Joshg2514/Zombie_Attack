using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
	
    void Start()
    {
        SelectedWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			selectedWeapon = 0;
			SelectedWeapon();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
		{
			selectedWeapon = 1;
			SelectedWeapon();
		}
		if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
		{
			selectedWeapon = 2;
			SelectedWeapon();
		}
		if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
		{
			selectedWeapon = 3;
			SelectedWeapon();
		}
		
		
    }
	void SelectedWeapon()
	{
		int i = 0;
		foreach(Transform weapon in transform)
		{
			if(i == selectedWeapon)
				weapon.gameObject.SetActive(true);
			else
				weapon.gameObject.SetActive(false);
			i++;
		}
			
}
}