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
        player = GameObject.Find("player");
        if (Input.GetKey(KeyCode.E))
        {
            player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
