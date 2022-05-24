using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakItem : MonoBehaviour
{
    public GameObject smashEffect;
    public AudioClip glass1, glass2, glass3;

    private void Start()
    {
        int randomNumber = Random.Range(1, 3);
        if(randomNumber == 1) smashEffect.GetComponent<AudioSource>().clip = glass1;
        if (randomNumber == 2) smashEffect.GetComponent<AudioSource>().clip = glass2;
        if (randomNumber == 3) smashEffect.GetComponent<AudioSource>().clip = glass3;
    }
    public void smash()
    {
        StartCoroutine(smashThis());
    }

    IEnumerator smashThis()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        smashEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
