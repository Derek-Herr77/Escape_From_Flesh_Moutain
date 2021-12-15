using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    Rigidbody[] colliders;
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

    public void forceRagdoll(Vector3 normal)
    {
        foreach (var rigidBody in colliders)
        {
            rigidBody.AddForce(normal * 5f, ForceMode.VelocityChange);
        }
    }


    // Update is called once per frame

    public void takeDamage(int damage, Vector3 normal)
    {
        health = health - damage;
        if(health <= 0)
        {
            enableRagdoll();
            forceRagdoll(normal);
        }
    }
}
