using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect_pickups : MonoBehaviour
{
    public Player_Ui player;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "pickup")
        {
            player.canPick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "pickup")
        {
            player.canPick = false;
        }

    }

}
