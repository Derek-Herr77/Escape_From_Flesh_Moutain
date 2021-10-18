using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fence_open : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E))
        {
            animator.SetTrigger("press");
        }
    }
}
