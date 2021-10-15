using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject glock;
    public Camera mainCamera;
    private Animator animator;
    public float zoom_fov;
    private float default_fov;
    public float damage = 30;
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

    // Update is called once per frame

    private void Start()
    {
        animator = glock.GetComponent<Animator>();
        default_fov = mainCamera.fieldOfView;
    }
    void Update()
    {
        gunOperations();
    }

    private void FixedUpdate()
    {
        while (Input.GetMouseButton(1) && mainCamera.fieldOfView >= zoom_fov)
        {
            mainCamera.fieldOfView = mainCamera.fieldOfView - 0.001f;
        }
        while (!Input.GetMouseButton(1) && mainCamera.fieldOfView <= default_fov)
        {
            mainCamera.fieldOfView += 0.01f;
        }
    }

    private void gunOperations()
    {
        if (Input.GetMouseButton(0))
        {
            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("glock_idle"))
            {
                shoot();  
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            flip();
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

    public void shoot()
    {
        animator.SetTrigger("fire");
        muzzle_flash.Play();
        muzzle_smoke.Play();


        //RAYCAST 2 LAYERS, THE GROUND AND EVERYTHING ELSE
        int layerMask = 1 << 0;
        int layerMask2 = 1 << 10;
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, layerMask) || Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, layerMask2))
        {
            Debug.Log(hit.transform.name);

            if(hit.rigidbody != null)
            {
                hit.transform.SendMessage("hitbyray");
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


}
