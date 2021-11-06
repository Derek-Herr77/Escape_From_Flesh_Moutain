using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_destroyed : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject cube_right;
    private GameObject cube_left;
    private ParticleSystem splash;
    void Start()
    {
        splash = GameObject.Find("splash").GetComponent<ParticleSystem>();
        cube_right = transform.GetChild(0).gameObject;
        cube_left = transform.GetChild(1).gameObject;
        StartCoroutine(addforce_time());
        StartCoroutine("delete_cubes");
        
       
    }
    IEnumerator delete_cubes()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    public void addforce()
    {
        cube_right.GetComponent<Rigidbody>().AddForce(transform.right * 100f);
        cube_left.GetComponent<Rigidbody>().AddForce(transform.right * -100f);
        cube_right.GetComponent<Rigidbody>().AddForce(transform.forward * -100f);
        cube_left.GetComponent<Rigidbody>().AddForce(transform.forward * -100f);
    }
    IEnumerator addforce_time()
    {
        yield return new WaitForSeconds(0.01f);
        addforce();
    }


}
