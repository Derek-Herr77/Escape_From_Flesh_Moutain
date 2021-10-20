using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class exit_script : MonoBehaviour
{
    private GameObject player;
    public TextMeshPro text;

    
    private void OnTriggerStay(Collider other)
    {
        player = GameObject.Find("player");
        if (Input.GetKey(KeyCode.E) && player.GetComponent<player_inventory>().player_key == true)
        {
            text.text = "Nice coc";
        }
    }

}
