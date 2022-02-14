using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_glock : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            player = other.transform.GetChild(0).GetChild(0).transform.gameObject;
             foreach (Transform child in player.transform)
            {
                if (child.name == "Glock_object")
                {
                    if (other.transform.GetComponent<player_inventory>().has_equiped_gun())
                    {
                        other.transform.GetComponent<player_inventory>().weapon_switch_pickup_secondary(child.gameObject);
                    }
                    else
                    {
                        other.transform.GetComponent<player_inventory>().set_secondary(child.gameObject);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
