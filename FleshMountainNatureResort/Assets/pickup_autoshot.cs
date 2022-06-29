using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_autoshot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    // Update is called once per frame
    public void pickup()
    {
        var player_read = player.transform.GetChild(0).GetChild(0).transform.gameObject;
        foreach (Transform child in player_read.transform)
        {
            if (child.name == "autoshot_object")
            {
                if (player.transform.GetComponent<player_inventory>().has_equiped_gun())
                {
                    player.transform.GetComponent<player_inventory>().weapon_switch_pickup(child.gameObject);
                }
                else
                {
                    player.transform.GetComponent<player_inventory>().set_primary(child.gameObject);
                }
            }
        }
        Destroy(gameObject);
    }
}