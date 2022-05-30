using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakItem : MonoBehaviour
{
    public GameObject smashEffect;
    public AudioClip glass1, glass2, glass3;
    public bool changeTexture = false;
    public bool deleteItem = true;
    public Material smashMat;
    public Texture smashTexture;
    public Material modifiedMat = null;


    private void Start()
    {
        int randomNumber = Random.Range(1, 4);
        if(randomNumber == 1) smashEffect.GetComponent<AudioSource>().clip = glass1;
        if (randomNumber == 2) smashEffect.GetComponent<AudioSource>().clip = glass2;
        if (randomNumber == 3) smashEffect.GetComponent<AudioSource>().clip = glass3;
    }
    public void smash()
    {
        if(deleteItem) StartCoroutine(smashThis());
        else
        {
            if(modifiedMat == null)
            {
                Debug.Log("Balls");
                modifiedMat = new Material(smashMat);
                modifiedMat.mainTexture = smashTexture;
                var copyMat = gameObject.GetComponent<Renderer>().materials;
                copyMat[1] = modifiedMat;
                gameObject.GetComponent<Renderer>().materials = copyMat;
            }
            smashEffect.SetActive(true);
        }
    }

    IEnumerator smashThis()
    {
        if(deleteItem) gameObject.GetComponent<Renderer>().enabled = false;
        smashEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        if(deleteItem) Destroy(gameObject);
    }
}
