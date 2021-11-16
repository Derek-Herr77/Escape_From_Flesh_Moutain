using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public GameObject button;
    public GameObject button2;
    public Animator animator;
    public float close_time = 5;
    public bool player_in = false;
    private bool time_up;


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            player_in = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player_in = false;
        }
    }

    private void Update()
    {
        Debug.Log(time_up);
        if (player_in == false && time_up == true)
        {
            animator.SetBool("zone", false);
        }
    }

    public void open_door()
    {
        time_up = false;
        animator.SetBool("zone", true);
        animator.Play("door_operate", -1, 0f);
        StartCoroutine(close_door());
        IEnumerator close_door()
        {
            yield return new WaitForSeconds(close_time);
            time_up = true;
        }
    }
}
