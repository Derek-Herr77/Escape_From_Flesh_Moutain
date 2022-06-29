using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickup_near : MonoBehaviour
{
    public GameObject player;
    public Camera player_camera;
    public float multiplier;
    public float solidDistance;
    public float maxDistance;
    public Image crosshair;
    public bool picked = false;

    // Update is called once per frame

    public void pickup()
    {
        if (transform.name == "glock_pickup") transform.GetComponent<pickup_glock>().pickup();
        else if(transform.name == "smg_pickup") transform.GetComponent<pickup_smg>().pickup();
        else if (transform.name == "autoshot_pickup") transform.GetComponent<pickup_autoshot>().pickup();
        else if (transform.name == "single_shotgun_pickup") transform.GetComponent<pickup_single_shotgun>().pickup();
        picked = true;
    }
}
