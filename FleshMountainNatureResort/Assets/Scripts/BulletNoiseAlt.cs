using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNoiseAlt : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<AudioSource>().pitch = Random.RandomRange(0.80f, 1);
    }
}
