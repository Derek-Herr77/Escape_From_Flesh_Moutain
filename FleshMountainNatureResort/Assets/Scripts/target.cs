using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class target : MonoBehaviour
{
    // Start is called before the first frame update
    public MonoBehaviour ai_script;
    public worker_audio sounds;
    public GameObject face;
    public GameObject blood_decal;
    public int health = 100;
    Rigidbody[] colliders;
    public Animator anim;
    private bool death_check = false;
    public GameObject headshoteffect;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    public void DisableRagdoll()
    {
        foreach (var rigidBody in colliders)
        {
            rigidBody.isKinematic = true;
        }
    }

    public void enableRagdoll()
    {
        foreach (var rigidBody in colliders)
        {
            rigidBody.isKinematic = false;
        }
    }

    public void forceRagdoll(Vector3 normal, float force_strength)
    {
        foreach (var rigidBody in colliders)
        {
            rigidBody.AddForce(normal * force_strength, ForceMode.VelocityChange);
        }
    }


    // Update is called once per frame

    //TAKE DAMAGE DISMEMBERMENT
    public void takeDamage(int damage, Vector3 normal, GameObject body_part, float force_strength)
    {
        if (death_check == false)
        {
            sounds.play_hit_noise();
            //check for hit animation
            if (transform.gameObject.name.Contains("man_worker"))
            {
                anim.Play("metarig|hit", -1, 0f);
            }
            health = health - damage;

            GameObject impactBlood = Instantiate(blood_decal, gameObject.transform.position, blood_decal.transform.rotation);
            impactBlood.SetActive(true);
            Destroy(impactBlood, 100f);
        }

            if (body_part.transform.name == "forearm.L")
            {
                body_part.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                body_part.GetComponent<Collider>().enabled = false;
            }


            if (health <= 0)
            {
                death();
                enableRagdoll();
                forceRagdoll(normal, force_strength);
            }
    }

    //TAKE DAMAGE NO DISMEBERMENT
    public void takeDamage(int damage, Vector3 normal,float force_strength)
    {
        if (death_check == false)
        {
            if(transform.gameObject.name == "man_worker_1")
            {
                anim.Play("metarig|hit", -1, 0f);
            }
            sounds.play_hit_noise();
            health = health - damage;

            GameObject impactBlood = Instantiate(blood_decal, gameObject.transform.position, blood_decal.transform.rotation);
            impactBlood.SetActive(true);
            Destroy(impactBlood, 100f);
            if (health <= 0)
            {
                death();
                enableRagdoll();
                forceRagdoll(normal, force_strength);
            }
        }
    }

    public void headshot(int damage, Vector3 normal, float force_strength)
    {
        if (death_check == false)
        {
            sounds.play_hit_noise();
        }

        if (health - (damage * 2) <= 0)
        {
            sounds.audio3_audio_spikes.enabled = false;
            face.transform.localScale = new Vector3(0f, 0f, 0f);
            headshoteffect.SetActive(true);
            enableRagdoll();
            forceRagdoll(normal, force_strength);
            deathHeadshot();
        }
        takeDamage(damage * 2, normal, force_strength);
    }

    public void death()
    {
        if (death_check == false)
        {
            sounds.play_death_noise(false);
            death_check = true;
            anim.enabled = false;
            ai_script.enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }
    public void deathHeadshot()
    {
        if (death_check == false)
        {
            sounds.play_death_noise(true);
            death_check = true;
            anim.enabled = false;
            ai_script.enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}
