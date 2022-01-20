using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_open : MonoBehaviour
{
    public GameObject door;
    public Animator animator;
    public Animator animator_button;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && !animator.GetCurrentAnimatorStateInfo(0).IsName("door_operate") && !animator.GetCurrentAnimatorStateInfo(0).IsName("door_operate_close") && !animator.IsInTransition(0) && other.tag == "Player")
        {
            animator_button.Play("button_press", -1, 0f);
            door.GetComponent<door>().open_door();
        }
    }
}

