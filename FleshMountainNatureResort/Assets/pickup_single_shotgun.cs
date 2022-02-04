using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_single_shotgun : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.tag == "Player")
        {
            player = other.transform.GetChild(0).GetChild(0).transform.gameObject;
            foreach (Transform child in player.transform)
            {
                if (child.name == "single_shotgun_object")
                {
                    other.transform.GetComponent<player_inventory>().set_primary(child.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
