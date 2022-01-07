using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood_decal_randomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float z_rotation = Random.Range(-180, 180);
        float scale_multiplier = Random.Range(0.1f, 0.3f);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scale_multiplier, gameObject.transform.localScale.y * scale_multiplier, gameObject.transform.localScale.z * scale_multiplier);
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, z_rotation);
    }
}
