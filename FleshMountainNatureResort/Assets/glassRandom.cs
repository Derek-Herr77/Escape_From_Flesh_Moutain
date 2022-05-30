using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassRandom : MonoBehaviour
{
    public void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.7f, 1f);
    }
}
