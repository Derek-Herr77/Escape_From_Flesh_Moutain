using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject cube;
    public float spawn_time = 0.5f;
    private bool spawning = false;
    private Vector3 pos;
    private float randx;
    private float randy;
    // Update is called once per frame
    void Update()
    {
        if(spawning == false)
        {
            StartCoroutine(spawn());
        }
        IEnumerator spawn()
        {
            spawning = true;
            yield return new WaitForSeconds(spawn_time);
            randx = Random.Range(-6f, 6f);
            randy = Random.Range(0f, 3f);
            pos = new Vector3(randx, randy, 0);
            Instantiate(cube, transform.position + pos, transform.rotation);
            spawning = false;
        }
    }
}
