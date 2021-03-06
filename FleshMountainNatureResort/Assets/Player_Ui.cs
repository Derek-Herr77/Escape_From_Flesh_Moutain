using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ui : MonoBehaviour
{
    public Camera player_camera;
    public Text health;
    public Text gunName;
    public Animator crosshair;
    public Image crosshair_image;
    public bool canPick;
    public Text ammo1;
    private player_inventory playerInv;

    private void Start()
    {
        playerInv = GetComponent<player_inventory>();
    }

    private void Update()
    {
        Ray ray = player_camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, 3, ~layerMask))
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
        else
        {
            crosshair.SetBool("hover", false);
        }
        if (canPick && crosshair_image.color.a <= 0.9) set_crosshair_on();
        if (!canPick && crosshair_image.color.a >= 0.1) set_crosshair_off();


        setAmmoUi();
        health.text = playerInv.health.ToString();
        if (playerInv.get_equiped_gun() != null)
        {
            var gun_name = playerInv.get_equiped_gun().name;
            if (gun_name == "Glock_object") { gunName.text = "Pistol"; }
            if (gun_name == "smg_object") { gunName.text = "Submachine Gun"; }
            if (gun_name == "single_shotgun_object") { gunName.text = "Single-shot Shotgun"; }
            if (gun_name == "autoshot_object") { gunName.text = "Semi-auto Shotgun"; }
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


    private void setAmmoUi()
    {
        string ammoline = "";
        if (playerInv.get_equiped_gun() != null)
        {
            string ammoName = playerInv.get_equiped_gun().name;
            if (ammoName.Contains("smg")) ammoline = playerInv.get_equiped_gun().GetComponentInChildren<smg_script>().ammo_in_magazine.ToString() + "/" + playerInv.return_smg_ammo().ToString();
            if (ammoName.Contains("shotgun")) ammoline = playerInv.get_equiped_gun().GetComponentInChildren<single_shotgun_script>().ammo_in_magazine.ToString() + "/" + playerInv.return_shotgun_ammo().ToString();
            if (ammoName.Contains("Glock")) ammoline = playerInv.get_equiped_gun().GetComponentInChildren<Glock_Script>().ammo_in_magazine.ToString() + "/" + playerInv.return_pistol_ammo().ToString();
            if (ammoName.Contains("autoshot")) ammoline = playerInv.get_equiped_gun().GetComponentInChildren<auto_shotgun_script>().ammo_in_magazine.ToString() + "/" + playerInv.return_shotgun_ammo().ToString();
            ammo1.text = ammoline;
        }
        else
        {
            string ammoLine = "";
            ammo1.text = ammoLine;
        }
    }
}
