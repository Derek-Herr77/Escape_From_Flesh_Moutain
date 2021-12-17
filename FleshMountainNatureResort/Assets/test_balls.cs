using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_balls : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject paint;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (paint.active)
            {
                paint.SetActive(false);
            }
            else
            {
                paint.SetActive(true);
            }
        }
    }
}
