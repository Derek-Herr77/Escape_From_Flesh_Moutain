using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ui : MonoBehaviour
{
    public Camera player_camera;
    public Animator crosshair;
    public Image crosshair_image;
    private bool canPick;

    private void Update()
    {
        if (canPick && crosshair_image.color.a <= 0.5) set_crosshair_on();
        if(!canPick && crosshair_image.color.a >= 0.5) set_crosshair_off();
        //
        Ray ray = player_camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.tag == "pickup")
            {
                crosshair.SetBool("hover", true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<pickup_near>().pickup();
                    canPick = false;
                }
            }
            else
            {
                crosshair.SetBool("hover", false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "pickup")
        {
            canPick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "pickup")
        {
            canPick = false;
        }

    }

    public void set_crosshair_on()
    {
        StartCoroutine(FadeTo(1, 0.4f));
    }

    public void set_crosshair_off()
    {
        StartCoroutine(FadeTo(0, 0.4f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = crosshair_image.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            crosshair_image.color = newColor;
            yield return null;
        }
    }
}
