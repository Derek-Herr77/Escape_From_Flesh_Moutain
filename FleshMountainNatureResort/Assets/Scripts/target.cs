using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void takeDamage(int damage)
    {
        health = health - damage;
        GetComponent<Test_behavior>().damage_taken = true;
    }
}
