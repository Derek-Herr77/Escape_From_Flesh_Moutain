using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spider_spawn : MonoBehaviour
{
    public GameObject player;
    public GameObject spider;
    public GameObject man_mesh;
    public float spider_delay;
    public float spider_chance_percent;
    private bool spawned = false;

    private void Update()
    {
        if(transform.gameObject.GetComponent<target>().health <= 0 && spawned == false)
        {
            player = GameObject.Find("player");
            spawned = true;
            spawn_spider();
        }
    }
    public void spawn_spider()
    {
        int spider_chance = Random.Range(0, 100);
        if (spider_chance <= spider_chance_percent)
        {
            StartCoroutine(spider_timer());
        }

        IEnumerator spider_timer()
        { 
            yield return new WaitForSeconds(spider_delay);
            if(IsAgentOnNavMesh(spider))
            {
                spider.transform.parent = null;
                spider.transform.LookAt(player.transform);
                spider.active = true;
                man_mesh.active = false;
            }
        }
    }


    public bool IsAgentOnNavMesh(GameObject agentObject)
    {
        Vector3 agentPosition = agentObject.transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(agentPosition, out hit, 3f, NavMesh.AllAreas))
        {
            if (Mathf.Approximately(agentPosition.x, hit.position.x)
                && Mathf.Approximately(agentPosition.z, hit.position.z))
            {
                Debug.Log(hit.position);
                return agentPosition.y >= hit.position.y;
            }
        }

        return false;
    }
}
