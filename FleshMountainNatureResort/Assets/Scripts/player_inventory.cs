using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_inventory : MonoBehaviour
{
    public int pistol_ammo = 20;
    public int smg_ammo = 100;
    public int shotgun_ammo = 5;
    // Update is called once per frame
    public int health = 100;
    private FirstPersonAIO player_script;
    public bool player_key = false;

    //weapon system
    private GameObject primary_gun = null;
    private GameObject secondary_gun = null;
    private GameObject equiped_gun = null;

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
        weapon_switch();
    }

    public void reload_pistol(int reload_amount)
    {
        if (pistol_ammo > 0)
        {
            pistol_ammo = pistol_ammo - reload_amount;
        }
    }

    public void reload_smg(int reload_amount)
    {
        if (smg_ammo > 0)
        {
            smg_ammo = smg_ammo - reload_amount;
        }
    }

    public void reload_shotgun(int reload_amount)
    {
        if (shotgun_ammo > 0)
        {
            shotgun_ammo = shotgun_ammo - reload_amount;
        }
    }

    public int return_pistol_ammo()
    {
        return pistol_ammo;
    }

    public int return_shotgun_ammo()
    {
        return shotgun_ammo;
    }

    public int return_smg_ammo()
    {
        return smg_ammo;
    }

    public void death()
    {
        player_script.enabled = false;
    }

    public void set_secondary(GameObject secondary)
    {
        secondary_gun = secondary;
        set_equiped(secondary_gun);
    }

    public void set_primary(GameObject primary)
    {
        primary_gun = primary;
        set_equiped(primary_gun);
    }

    public void set_equiped(GameObject gun)
    {
        if(equiped_gun != null)
        {
            equiped_gun.SetActive(false);
            equiped_gun = gun;
            equiped_gun.SetActive(true);
        }
        else
        {
            equiped_gun = gun;
            equiped_gun.SetActive(true);
        }
    }
    public void weapon_switch()
    {
        //weapon switching
        if (Input.mouseScrollDelta.y != 0f && primary_gun != null && secondary_gun != null 
            && !equiped_gun.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("switch") 
            && !equiped_gun.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("non_switchable"))
        {
            equiped_gun.transform.GetChild(0).GetComponent<Animator>().SetTrigger("switch");
        }

        if (equiped_gun != null)
        {
            if (equiped_gun.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("switch"))
            {
                if (equiped_gun == primary_gun)
                {
                    set_equiped(secondary_gun);
                }
                else
                {
                    set_equiped(primary_gun);
                }
            }
        }
    }

    public void weapon_switch_pickup_primary(GameObject primary)
    {
        primary_gun = primary;
        equiped_gun.transform.GetChild(0).GetComponent<Animator>().SetTrigger("switch");

        if(equiped_gun != null)
        {
            if (equiped_gun.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("switch"))
            {
                 set_equiped(primary_gun);
            }
        }
    }

    public void weapon_switch_pickup_secondary(GameObject secondary)
    {
        secondary_gun = secondary;
        equiped_gun.transform.GetChild(0).GetComponent<Animator>().SetTrigger("switch");

        if (equiped_gun != null)
        {
            if (equiped_gun.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("switch"))
            {
                set_equiped(secondary_gun);
            }
        }
    }

    public bool has_equiped_gun()
    {
        if(equiped_gun != null)
        {
            return true;
        }
        return false;
    }

    public GameObject get_equiped_gun()
    {
        return equiped_gun;
    }
}

