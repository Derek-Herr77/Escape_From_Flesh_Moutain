﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_glock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    // Update is called once per frame
    public void pickup()
    {
        var player_read = player.transform.GetChild(0).GetChild(0).transform.gameObject;
        foreach (Transform child in player_read.transform)
        {
            if (child.name == "Glock_object")
            {
                if (player.transform.GetComponent<player_inventory>().has_equiped_gun())
                {
                    player.transform.GetComponent<player_inventory>().weapon_switch_pickup_secondary(child.gameObject);
                }
                else
                {
                    player.transform.GetComponent<player_inventory>().set_secondary(child.gameObject);
                }
            }
        }
        Destroy(gameObject);
    }
}
