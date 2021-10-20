using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_prot : MonoBehaviour
{
    public GameObject gun1, gun2, gun3;
    public GameObject key1, key2, key3;

    private void Start()
    {
        int random1 = Random.Range(0, 9);
        int random2 = Random.Range(0, 9);

        if(random1 <= 3)
        {
            gun1.SetActive(true);
        }
        else if(random1 <= 6 && random1 > 3)
        {
            gun2.SetActive(true);
        }
        else
        {
            gun3.SetActive(true);
        }

        if (random2 <= 3)
        {
            key1.SetActive(true);
        }
        else if (random2 <= 6 && random2 > 3)
        {
            key2.SetActive(true);
        }
        else
        {
            key3.SetActive(true);
        }

    }
}
