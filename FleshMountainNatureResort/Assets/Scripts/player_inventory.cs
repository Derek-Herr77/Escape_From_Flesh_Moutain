using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_inventory : MonoBehaviour
{
    public int pistol_ammo = 20;
    // Update is called once per frame
    public int health = 100;
    private FirstPersonAIO player_script;
    public bool player_key = false;
    private void Start()
    {
        player_script = GetComponent<FirstPersonAIO>();
    }
    void Update()
    {
        if (health <= 0)
        {
            death();
        }
    }

    public void reload_pistol(int reload_amount)
    {
        if (pistol_ammo > 0)
        {
            pistol_ammo = pistol_ammo - reload_amount;
        }
        Debug.Log(pistol_ammo);
    }

    public int return_pistol_ammo()
    {
        return pistol_ammo;
    }


    public void death()
    {
        player_script.enabled = false;
    }
}
