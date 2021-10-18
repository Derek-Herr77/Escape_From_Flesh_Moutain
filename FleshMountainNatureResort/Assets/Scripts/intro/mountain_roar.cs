using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountain_roar : MonoBehaviour
{
    public AudioSource roar;
    public GameObject shake;
    public GameObject shake2;
    private int played = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(played == 0)
        {
            shake.GetComponent<TraumaInducer>().enabled = true;
            StartCoroutine(shake_again());
            roar.Play();
            played += 1;

           
        }


        IEnumerator shake_again()
        { 
            yield return new WaitForSeconds(1.7f);
            shake2.GetComponent<TraumaInducer>().enabled = true;
        }
    }

}
