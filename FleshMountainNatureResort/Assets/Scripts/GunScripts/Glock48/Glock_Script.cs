using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject glock;
    public GameObject player;
    public Camera mainCamera;
    private Animator animator;
    public float zoom_fov;
    private float default_fov;
    public int damage = 30;
    public ParticleSystem muzzle_flash;
    public ParticleSystem muzzle_smoke;
    public AudioSource gun;
    public AudioSource flip_sound;
    public AudioSource shell;
    public AudioClip gunshot;
    public AudioClip shell_casing_sound;
    public AudioClip flip_swoosh;
    public GameObject impact;
    public int decal_count = 15;
    //ammo and magazine
    public int magazine_size = 10;
    public int ammo_in_magazine = 10;
    public float recoil_strength;
    public float recoil_max;
    //
    //recoil
    Vector3 orig_camera_rotation;
    public Vector3 up_recoil;

    // Update is called once per frame

    private void Start()
    {
        player = GameObject.Find("player");
        animator = glock.GetComponent<Animator>();
        default_fov = mainCamera.fieldOfView;
    }
    void Update()
    {
        gunOperations();
    }

    private void gunOperations()
    {

        if(Input.GetMouseButton(1))
        {
            animator.SetBool("aim", true);
        }
        else
        {
            animator.SetBool("aim", false);
        }
        if (Input.GetMouseButton(0))
        {
            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("glock_idle") && ammo_in_magazine > 0)
            {
                shoot();
                recoil();
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            int player_ammo = player.GetComponent<player_inventory>().return_pistol_ammo();
            if(player_ammo > 0)
            {
                flip();
                reload();
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        
    }

    public void reload()
    {
        int reload_amount = magazine_size - ammo_in_magazine;
        int player_ammo = player.GetComponent<player_inventory>().return_pistol_ammo();

        if(reload_amount >= player_ammo)
        {
            reload_amount = player_ammo;
            ammo_in_magazine = ammo_in_magazine + player_ammo;
            player.GetComponent<player_inventory>().reload_pistol(reload_amount);
        }
        else
        {
            ammo_in_magazine = ammo_in_magazine + reload_amount;
            player.GetComponent<player_inventory>().reload_pistol(reload_amount);
        }
    }

    public void shoot()
    {
        if(ammo_in_magazine == 1)
        {
            animator.SetTrigger("fire_last");
        }
        else
        {
            animator.SetTrigger("fire");
        }
        muzzle_flash.Play();
        muzzle_smoke.Play();


        //RAYCAST 2 LAYERS, THE GROUND AND EVERYTHING ELSE
        int layerMask = 1 << 0;
        int layerMask2 = 1 << 10;
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, layerMask) || Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, layerMask2))
        {
       
            if(hit.rigidbody != null)
            {
                if(hit.transform.GetComponent<target>() != null)
                {
                    target target = hit.transform.GetComponent<target>();
                    target.takeDamage(damage);
                }
                if(hit.transform.tag == "enemy")
                {
                    hit.transform.SendMessage("hitbyray");
                }
                hit.rigidbody.AddForce(-hit.normal * 1f, ForceMode.Impulse);
            }
                GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO.transform.parent = hit.transform;
                Destroy(impactGO, 10f);
        }

        StartCoroutine(shell_casing());
        IEnumerator shell_casing()
        {
            gun.PlayOneShot(gunshot);
            yield return new WaitForSeconds(0.1f);
            shell.PlayOneShot(shell_casing_sound);

        }

        ammo_in_magazine -= 1;
    }
    public void flip()
    {
        animator.Play("glock_flip", -1, 0);
        flip_sound.PlayOneShot(flip_swoosh);
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("glock_flip"))
        {
            animator.Play("glock_flip", -1, 0.2f);
        }
    }

    public void recoil()
    {
       player.GetComponent<FirstPersonAIO>().addRecoil(recoil_strength, 10);
    }


}
