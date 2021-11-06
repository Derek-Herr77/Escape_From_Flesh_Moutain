using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_open : MonoBehaviour
{
    public Animator animator;
    private Animator animator_button;
    public float close_time = 5;
    public bool player_in =false;
    private bool time_up;

    private void Start()
    {
        animator_button = GetComponent<Animator>();
    }

    private void Update()
    {
        if(player_in == false && time_up == true)
        {
            animator.SetBool("zone", false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && !animator.GetCurrentAnimatorStateInfo(0).IsName("door_operate") && !animator.GetCurrentAnimatorStateInfo(0).IsName("door_operate_close") && !animator.IsInTransition(0))
        {
            time_up = false;
            animator.SetBool("zone", true);
            animator.Play("door_operate", -1, 0f);
            animator_button.Play("button_press", -1, 0f);
            StartCoroutine(close_door());
            IEnumerator close_door()
            {
                yield return new WaitForSeconds(close_time);
                time_up = true;
            }
        }
    }
}

