using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class man_worker_ai : MonoBehaviour
{
    Rigidbody[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();

    }

    public void DisableRagdoll()
    {
        foreach(var rigidBody in colliders)
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
}
