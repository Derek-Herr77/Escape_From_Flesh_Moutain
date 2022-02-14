using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class single_shotgun_script : MonoBehaviour
{
    // Start is called before the first frame update
    private player_audio_manager player_sounds;
    public GameObject gun;
    public GameObject player;
    public Camera mainCamera;
    private Animator animator;
    public int damage = 50;
    public ParticleSystem muzzle_flash;
    public ParticleSystem muzzle_smoke;
    public AudioSource gun_sounds;
    public AudioSource reload_sound_source;
    public AudioSource shell;
    public AudioClip gunshot;
    public AudioClip reload_noise;
    public AudioClip reload_noise_2;
    public AudioClip shell_casing_sound;
    public AudioClip empty_fire;
    public GameObject impact;
    public GameObject impactBlood;
    public GameObject blood_decals;
    public int decal_count = 15;
    //ammo and magazine
    public int magazine_size = 1;
    public int ammo_in_magazine = 1;
    public float recoil_strength;
    public float force_strength = 4f;
    //

    // Update is called once per frame

    private void Start()
    {
        player = GameObject.Find("player");
        player_sounds = player.GetComponent<player_audio_manager>();
        animator = gun.GetComponent<Animator>();
    }
    void Update()
    {
        gunOperations();
        animator.SetInteger("ammo", ammo_in_magazine);
        //switch gun
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        // switch_gun();
        //}
    }

    private void gunOperations()
    {

        if (Input.GetMouseButton(1))
        {
            gun_aim();
        }
        else
        {
            gun_unaim();
        }
        if (Input.GetMouseButton(0))
        {
            if (!animator.IsInTransition(0) && ammo_in_magazine > 0 && (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("aim")) && animator.GetBool("reload") != true && player.GetComponent<FirstPersonAIO>().isSprinting == false)
            {
                shoot();
                recoil();
            }
            if (ammo_in_magazine == 0 && (animator.GetCurrentAnimatorStateInfo(0).IsName("idle_empty") || animator.GetCurrentAnimatorStateInfo(0).IsName("aim_empty")) && !animator.IsInTransition(0) && animator.GetBool("reload") != true && player.GetComponent<FirstPersonAIO>().isSprinting == false)
            {
                shoot_empty();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            int player_ammo = player.GetComponent<player_inventory>().return_shotgun_ammo();
            if (player_ammo > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("reload") && !animator.GetCurrentAnimatorStateInfo(0).IsName("reload_not_empty"))
            {
                reload_animation();
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
    }


    public void reload_sounds()
    {
        int reload_amount = magazine_size - ammo_in_magazine;

        if (reload_amount > 0)
        {
            if (ammo_in_magazine == 0)
            {
                gun_sounds.PlayOneShot(reload_noise);
            }
            else
            {
                gun_sounds.PlayOneShot(reload_noise_2);
            }
        }
    }

    public void reload_script()
    {
        int reload_amount = magazine_size - ammo_in_magazine;
        int player_ammo = player.GetComponent<player_inventory>().return_shotgun_ammo();

        if (reload_amount > 0)
        {
            if (reload_amount >= player_ammo)
            {
                animator.SetBool("empty", true);
                reload_amount = player_ammo;
                ammo_in_magazine = ammo_in_magazine + player_ammo;
                player.GetComponent<player_inventory>().reload_shotgun(reload_amount);
            }
            else
            {
                animator.SetBool("empty", true);
                ammo_in_magazine = ammo_in_magazine + reload_amount;
                player.GetComponent<player_inventory>().reload_shotgun(reload_amount);
            }
        }
    }

    public void shoot()
    {
        //if (ammo_in_magazine == 1)
        //{
        //animator.SetTrigger("fire_last");
        //}

        //else
        //{
        animator.SetTrigger("fire");
        //}
        ammo_in_magazine -= 1;
        if(ammo_in_magazine <= 0)
        {
            animator.SetBool("empty", true);
        }
        muzzle_flash.Play();
        muzzle_smoke.Play();

        StartCoroutine(shell_casing());
        IEnumerator shell_casing()
        {
            gun_sounds.PlayOneShot(gunshot);
            yield return new WaitForSeconds(0.1f);
            shell.PlayOneShot(shell_casing_sound);
        }

        //RAYCAST 2 LAYERS, THE GROUND AND EVERYTHING ELSE
        //int layerMask = 1 << 12; //ground layer not used
        int layerMask2 = 1 << 0;
        int layerMask3 = 1 << 11;
        int layerMask1 = 1 << 12;
        RaycastHit hit;
        RaycastHit hit_blood;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, layerMask1 | layerMask2 | layerMask3))
        {
            //HIT FORCE
            if (hit.rigidbody != null)
            {
                if (hit.transform.GetComponent<target>() != null || hit.transform.GetComponentInParent<target>() != null)
                {
                    target target = hit.transform.GetComponentInParent<target>();
                    if (hit.transform.name == "head")
                    {
                        target.headshot(damage, -hit.normal, force_strength);
                        player_sounds.play_headshot_crack();
                    }
                    else
                    {
                        target.takeDamage(damage, -hit.normal, hit.transform.gameObject, force_strength);
                    }
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * force_strength, ForceMode.Impulse);
                }
            }
            //

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                if (hit.collider.enabled == true)
                {
                    GameObject impactBlood_1 = Instantiate(impactBlood, hit.point, Quaternion.LookRotation(hit.normal));
                    impactBlood_1.SetActive(true);
                    impactBlood_1.transform.parent = hit.transform;
                    Destroy(impactBlood_1, 100f);
                }

                if (Physics.Raycast(hit.transform.position, mainCamera.transform.forward, out hit_blood, 8f, layerMask1 | layerMask2))
                {
                    if (hit_blood.transform.tag == "no_decal")
                    {
                        GameObject impactBlood_2 = Instantiate(impactBlood, hit_blood.point, Quaternion.LookRotation(hit_blood.normal));
                        impactBlood_2.transform.parent = hit_blood.transform;
                        impactBlood_2.SetActive(true);
                        Destroy(impactBlood_2, 100f);
                    }
                    else
                    {
                        GameObject impactBlood_2 = Instantiate(blood_decals, hit_blood.point, Quaternion.LookRotation(-hit_blood.normal));
                        impactBlood_2.transform.parent = hit_blood.transform;
                        impactBlood_2.SetActive(true);
                        Destroy(impactBlood_2, 100f);
                    }
                }
            }
            else
            {
                GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO.transform.parent = hit.transform;
                impactGO.SetActive(true);
                Destroy(impactGO, 10f);
            }
        }

    }
    public void reload_animation()
    {
        if (!animator.IsInTransition(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("empty_reload") && !animator.GetCurrentAnimatorStateInfo(0).IsName("reload") && (ammo_in_magazine != magazine_size) && animator.GetBool("reload") != true)
        {
            animator.SetTrigger("reload");
            gun_unaim();
        }
    }

    public void recoil()
    {
        player.GetComponent<FirstPersonAIO>().addRecoil(recoil_strength);
    }

    public void gun_aim()
    {
        if (animator.GetBool("reload") == false)
        {
            animator.SetBool("aim", true);
            player.GetComponent<FirstPersonAIO>().aiming = true;
        }
    }

    public void gun_unaim()
    {
        animator.SetBool("aim", false);
        player.GetComponent<FirstPersonAIO>().aiming = false;
    }

    public void shoot_empty()
    {
        animator.SetTrigger("fire_empty");
        gun_sounds.PlayOneShot(empty_fire);
    }


    public void isWalking_check()
    {
        if ((Mathf.Abs(player.GetComponent<FirstPersonAIO>().fps_Rigidbody.velocity.x) > 0.05f || Mathf.Abs(player.GetComponent<FirstPersonAIO>().fps_Rigidbody.velocity.z) > 0.05f) && !player.GetComponent<FirstPersonAIO>().aiming == true)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    //public void switch_gun()
    //{
    //animator.SetTrigger("switch");

    /// <summary>
    /// This section of animation checks if the switch was successful before switching the gun. This prevents the animation from clipping when switching a gun
    /// </summary>
    public void set_check_true()
    {
        animator.SetBool("check", true);
    }

    public void set_check_false()
    {
        animator.SetBool("check", false);
    }

}
