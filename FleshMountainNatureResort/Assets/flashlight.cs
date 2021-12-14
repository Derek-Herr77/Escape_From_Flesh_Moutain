using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour
{
    public GameObject power;
    public AudioSource button_noise;
    public AudioClip button_sound;
    private bool buttonlimit = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (buttonlimit)
            {
                buttonlimit = false;
                button_noise.PlayOneShot(button_sound);
                if (power.GetComponent<Light>().isActiveAndEnabled == true) { power.GetComponent<Light>().enabled = false; }
                else { power.GetComponent<Light>().enabled = true; }
                StartCoroutine(flashlight_system());
            }
        }

        IEnumerator flashlight_system()
        {
            yield return new WaitForSeconds(1f);
            buttonlimit = true;
        }

    }
}
