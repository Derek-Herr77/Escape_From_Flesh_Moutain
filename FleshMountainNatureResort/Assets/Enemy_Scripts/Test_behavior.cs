using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Test_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent enemy;
    public Transform player;
    public Animator animator;

    public LayerMask whatsGround, whatsPlayer;

    //wander
    public Vector3 walk_point;
    bool walk_set;
    public float walk_point_range = 15;
    public float walk_delay;

    //attack
    public float attack_delay;
    bool has_attacked;
    bool cr_running = false;
    public bool damage_taken = false;
    public bool chased;

    //states
    public float sightRange, attackRange;
    public bool player_insight, player_inattack;

    // Update is called once per frame

    private void Awake()
    {
        player = GameObject.Find("player").transform;
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        player_insight = Physics.CheckSphere(transform.position, sightRange, whatsPlayer);
        player_inattack = Physics.CheckSphere(transform.position, attackRange, whatsPlayer);

        if (destination_reached())
        {
            walk_set = false;
        }

        if(walk_set == false)
        {
            animator.SetFloat("speed", 0, 0.3f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("speed", 1, 0.3f, Time.deltaTime);
        }

        if (!player_insight && !player_inattack) wander();
        if ((player_insight && !player_inattack) || damage_taken) chase();
        if (player_insight && player_inattack) attack();

        Debug.Log(animator.GetFloat("speed"));
    }

    private void wander()
    {
        if(chased == true)
        {
            walk_set = false;
            chased = false;
        }
        if (!walk_set)
        {
            //Invoke(nameof(find_walkpoint), walk_delay);
            //Invoke(nameof(setpoint), walk_delay);
            if (cr_running == false)
            {
                StartCoroutine(walk_delay_set());
            }
        }

        IEnumerator walk_delay_set()
        {
            cr_running = true;
            yield return new WaitForSeconds(walk_delay);
            walk_set = true;
            setpoint();
            cr_running = false;
        }

    }


    //OBSOLETE FUNCTION
    /*
    private void find_walkpoint()
    {
        while (walk_set == false)
        {
            float randomZ = Random.Range(-walk_point_range + 5, walk_point_range);
            float randomX = Random.Range(-walk_point_range + 5, walk_point_range);

            walk_point = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walk_point, -transform.up, 2f, whatsGround))
            {
                walk_set = true;
                animator.SetBool("seen", true);
                enemy.SetDestination(walk_point);
            }
        }
    }
    */

    private void chase()
    {
        chased = true;
        walk_set = true;
        enemy.speed = 2.3f;
        enemy.SetDestination(player.position);
    }

    private void attack()
    {
        Debug.Log("ATTACKING");
        //stop enemy
        enemy.SetDestination(transform.position);

        transform.LookAt(player);

        if(!has_attacked)
        {
            animator.SetTrigger("attack");
            has_attacked = true;
            player.gameObject.GetComponent<player_inventory>().health = player.gameObject.GetComponent<player_inventory>().health - 50;

            Invoke(nameof(ResetAttack), attack_delay);
        }
    }

    private void ResetAttack()
    {
        has_attacked = false;
    }


    //slow down on shot
    void hitbyray()
    {
        //not for big guy
        /*
        StartCoroutine(turnoff());
        IEnumerator turnoff()
        {
            enemy.isStopped = true;
            yield return new WaitForSeconds(0.5f);
            enemy.ResetPath();
            enemy.SetDestination(walk_point);
        }
        */
    }


    public void setpoint()
    {
        walk_point = RandomNavmeshLocation(15f);
        enemy.SetDestination(walk_point);
        Debug.Log("setpoint");
    }
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        finalPosition = hit.position;

        return finalPosition;
    }

    public bool destination_reached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, walk_point);
        if (distanceToTarget <= 1.5f)
        {
            Debug.Log("point reached");
            return true;
        }
        else
        {
            return false;
        }
    }

}
