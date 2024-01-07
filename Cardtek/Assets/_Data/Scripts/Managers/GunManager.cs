using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using Sirenix.Serialization;
using UnityEngine;

public class GunManager : MonoBehaviour
{
      
    public static GunManager Instance;

    private int actualGunIndex = 0;

    public List<Gun> gunInventory;

    private float changeGun;

    private void Awake() {
        Instance = this;

    }

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        changeGun = PlayerInputManager.Instance.GetChangeGunAxis();

        int previousGunIndex = actualGunIndex;
        
        if(changeGun > 0f)
        {
            if (actualGunIndex >= gunInventory.Count - 1)
            {
                actualGunIndex = 0;
            }
            else
            {
                actualGunIndex++;
            }
                
        }
        if (changeGun < 0f)
        {
            if (actualGunIndex <= 0)
            {
                actualGunIndex = gunInventory.Count - 1;
            }
            else
            {
                actualGunIndex--;
            }
        }

        if (actualGunIndex != previousGunIndex)
        {
            SelectWeapon();
        }

    }

    private void AddGun(Gun gun)
    {

        gunInventory.Add(gun);

    }
    
    private void DropGun()
    {

        gunInventory.RemoveAt(actualGunIndex);

    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Gun weapon in gunInventory)
        {

            if (i == actualGunIndex)
            {
                weapon.gameObject.SetActive(true);
                
                
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
       
            i++;
        }

    }

}
