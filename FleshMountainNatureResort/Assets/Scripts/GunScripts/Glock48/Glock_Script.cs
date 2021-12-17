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
    public int damage = 30;
    public ParticleSystem muzzle_flash;
    public ParticleSystem muzzle_smoke;
    public AudioSource gun;
    public AudioSource reload_sound_source;
    public AudioSource shell;
    public AudioClip gunshot;
    public AudioClip reload_noise;
    public AudioClip reload_noise_2;
    public AudioClip shell_casing_sound;
    public AudioClip flip_swoosh;
    public AudioClip empty_fire;
    public GameObject impact;
    public GameObject impactBlood;
    public GameObject blood_decals;
    public int decal_count = 15;
    //ammo and magazine
    public int magazine_size = 10;
    public int ammo_in_magazine = 10;
    public float recoil_strength;
    //
    public Vector3 up_recoil;

    // Update is called once per frame

    private void Start()
    {
        player = GameObject.Find("player");
        animator = glock.GetComponent<Animator>();
    }
    void Update()
    {
        gunOperations();
    }

    private void gunOperations()
    {

        if(Input.GetMouseButton(1))
        {
            glock_aim();
        }
        else
        {
            glock_unaim();
        }
        if (Input.GetMouseButton(0))
        {
            if (!animator.IsInTransition(0) && ammo_in_magazine > 0 && (animator.GetCurrentAnimatorStateInfo(0).IsName("glock_idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("glock_aim")) && animator.GetBool("flip") != true && player.GetComponent<FirstPersonAIO>().isSprinting == false)
            {
                shoot();
                recoil();
            }
            if(ammo_in_magazine == 0 && (animator.GetCurrentAnimatorStateInfo(0).IsName("glock_idle_empty")|| animator.GetCurrentAnimatorStateInfo(0).IsName("glock_empty_aim")) && !animator.IsInTransition(0) && animator.GetBool("flip") != true && player.GetComponent<FirstPersonAIO>().isSprinting == false)
            {
                    shoot_empty();
             }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            int player_ammo = player.GetComponent<player_inventory>().return_pistol_ammo();
            if(player_ammo > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("glock_flip") && !animator.GetCurrentAnimatorStateInfo(0).IsName("reload_not_empty"))
            {
                flip();
            }
        }

        if (player.GetComponent<FirstPersonAIO>().isSprinting == true)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        isWalking_check();


        if(animator.GetCurrentAnimatorStateInfo(0).IsName("glock_flip") || animator.GetCurrentAnimatorStateInfo(0).IsName("reload_not_empty") || animator.GetCurrentAnimatorStateInfo(0).IsName("reload_not_empty 0"))
        {
            reload();
        }
    }

    public void reload()
    {
        int reload_amount = magazine_size - ammo_in_magazine;
        int player_ammo = player.GetComponent<player_inventory>().return_pistol_ammo();

        if (reload_amount > 0)
        {
            if (ammo_in_magazine == 0)
            {
                gun.PlayOneShot(reload_noise);
            }
            else
            {
                gun.PlayOneShot(reload_noise_2);
            }
            if (reload_amount >= player_ammo)
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
        ammo_in_magazine -= 1;
        muzzle_flash.Play();
        muzzle_smoke.Play();


        //RAYCAST 2 LAYERS, THE GROUND AND EVERYTHING ELSE
        //int layerMask = 1 << 12; //ground layer not used
        int layerMask2 = 1 << 0;
        int layerMask3 = 1 << 11;
        RaycastHit hit;
        RaycastHit hit_blood;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, layerMask2 | layerMask3))
        {
                if(hit.rigidbody != null)
                {
                    if (hit.transform.GetComponent<target>() != null || hit.transform.root.GetComponent<target>() != null)
                    {
                        target target = hit.transform.root.GetComponent<target>();
                        target.takeDamage(damage, -hit.normal);
                    }
                    hit.rigidbody.AddForce(-hit.normal * 15f, ForceMode.Impulse);
                }

                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
                {
                    GameObject impactBlood_1 = Instantiate(impactBlood, hit.point, Quaternion.LookRotation(hit.normal));
                    impactBlood_1.transform.parent = hit.transform;
                    Destroy(impactBlood_1, 100f);
                    if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit_blood, Mathf.Infinity, layerMask2))
                    {
                        if (hit_blood.transform.tag == "no_decal")
                        {
                            GameObject impactBlood_2 = Instantiate(impactBlood, hit_blood.point, Quaternion.LookRotation(hit_blood.normal));
                            impactBlood_2.transform.parent = hit_blood.transform;
                            Destroy(impactBlood_2, 100f);
                        }
                        else
                        {
                            GameObject impactBlood_2 = Instantiate(blood_decals, hit_blood.point, Quaternion.LookRotation(-hit_blood.normal));
                            impactBlood_2.transform.parent = hit_blood.transform;
                            impactBlood_2.SetActive(true);
                            float scale_multiplier = Random.Range(0.5f, 2);
                            impactBlood_2.transform.localScale = new Vector3(impactBlood_2.transform.localScale.x * scale_multiplier, impactBlood_2.transform.localScale.y * scale_multiplier, impactBlood_2.transform.localScale.z * scale_multiplier);
                            Destroy(impactBlood_2, 100f);
                        }
                    }
                }
                else
                {
                    GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                    impactGO.transform.parent = hit.transform;
                    Destroy(impactGO, 10f);
                }
        }

        StartCoroutine(shell_casing());
        IEnumerator shell_casing()
        {
            gun.PlayOneShot(gunshot);
            yield return new WaitForSeconds(0.1f);
            shell.PlayOneShot(shell_casing_sound);
        }

    }
    public void flip()
    {
        if (!animator.IsInTransition(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("empty_reload") && !animator.GetCurrentAnimatorStateInfo(0).IsName("flip") && (ammo_in_magazine != magazine_size) && animator.GetBool("flip") != true)
        { 
          animator.SetTrigger("flip");
          glock_unaim();
        }
    }

    public void recoil()
    {
       player.GetComponent<FirstPersonAIO>().addRecoil(recoil_strength);
    }

    public void glock_aim()
    {
        if (animator.GetBool("flip") == false)
        {
            animator.SetBool("aim", true);
            player.GetComponent<FirstPersonAIO>().aiming = true;
        }
    }

    public void glock_unaim()
    {
        animator.SetBool("aim", false);
        player.GetComponent<FirstPersonAIO>().aiming = false;
    }

    public void shoot_empty()
    {
        animator.SetTrigger("fire_empty");
        gun.PlayOneShot(empty_fire);
    }


    public void isWalking_check()
    {
        if ((Mathf.Abs(player.GetComponent<FirstPersonAIO>().fps_Rigidbody.velocity.x) > 0.01f || Mathf.Abs(player.GetComponent<FirstPersonAIO>().fps_Rigidbody.velocity.z) > 0.01f) && !player.GetComponent<FirstPersonAIO>().aiming == true)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }
    
}
