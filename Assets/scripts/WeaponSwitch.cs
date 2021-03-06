﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// klasa odpowiedzialna za wmianę broni 
/// </summary>
public class WeaponSwitch : MonoBehaviour
{
    [SerializeField]
    List<weapon> weapons;
    int previousWeapon = 0;
    int selectedWeapon = 0;

    public List<weapon> Weapons { get => weapons; set => weapons = value; }
    public int SelectedWeapon { get => selectedWeapon; set => selectedWeapon = value; }

    void Start()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeapon)
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //scrol zmianabroni
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            do
            {
                selectedWeapon = (selectedWeapon + 1) % weapons.Count;
            } while (weapons[selectedWeapon].GetComponent<weapon>().InOwned == false);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            do
            {
                selectedWeapon = (((selectedWeapon - 1) + weapons.Count) % weapons.Count);
            } while (weapons[selectedWeapon].GetComponent<weapon>().InOwned == false);
        for(int i=49;i<49+weapons.Count;i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                selectedWeapon = i-49; //49 Alpha1
            }
        }
        if (previousWeapon != selectedWeapon)
        {
            switchWepon(selectedWeapon);
        }

    }
    public void switchWepon(int toWeapon)
    {
        if(toWeapon<weapons.Count&&weapons[toWeapon].InOwned==true)
        {
            weapons[previousWeapon].gameObject.SetActive(false);
            weapons[toWeapon].gameObject.SetActive(true);
             previousWeapon = toWeapon;
            selectedWeapon = toWeapon;
        }

    }
    public void setWeaponActive(int type)
    {
        weapons[type].InOwned = true;
        switchWepon(type);

        Transform parent = transform.parent;
        Transform transform1 = parent.Find("Canvas");
        Transform transform2 = transform1.Find("UIWeapons");
        UiWeaponsControler UiWeaponsControler = transform2.GetComponent<UiWeaponsControler>();
                  UiWeaponsControler.update();
    }
}
