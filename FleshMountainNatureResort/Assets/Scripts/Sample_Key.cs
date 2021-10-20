using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Key : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        player = GameObject.Find("player");
        if (Input.GetKey(KeyCode.E))
        {
            player.GetComponent<player_inventory>().player_key = true;
            Destroy(gameObject);
        }
    }
}
