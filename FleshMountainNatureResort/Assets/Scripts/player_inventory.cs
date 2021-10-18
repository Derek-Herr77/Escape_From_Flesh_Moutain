using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_inventory : MonoBehaviour
{
    public int pistol_ammo = 20;
    // Update is called once per frame
    void Update()
    {
    }

    public void reload_pistol(int reload_amount)
    {
        if(pistol_ammo > 0)
        {
            pistol_ammo = pistol_ammo - reload_amount;
        }
        Debug.Log(pistol_ammo);
    }

    public int return_pistol_ammo()
    {
        return pistol_ammo;
    }    
}
