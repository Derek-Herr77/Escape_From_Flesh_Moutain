using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public GameObject button;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            button.GetComponent<door_open>().player_in = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            button.GetComponent<door_open>().player_in = false;
        }
    }

}
