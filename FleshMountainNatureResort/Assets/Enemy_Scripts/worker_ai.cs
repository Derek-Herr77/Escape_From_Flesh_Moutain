using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class worker_ai : MonoBehaviour
{
    // Start is called before the first frame update
    public worker_audio sounds;
    public NavMeshAgent enemy;
    public Transform player;
    public Animator animator;

    public LayerMask whatsGround, whatsPlayer;

    //wander
    public Vector3 walk_point;
    bool walk_set;
    public float walk_point_range = 15;
    public float walk_delay;
    public float walk_speed = 1;

    //attack
    public float attack_delay;
    bool has_attacked;
    bool cr_running = false;
    public bool damage_taken = false;
    public bool chased;
    public float chase_speed = 3;

    //states
    public float sightRange, attackRange;
    public bool player_insight, player_inattack;

    // Update is called once per frame

    private void Awake()
    {
        player = GameObject.Find("player").transform;
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        walk_delay = Random.Range(1, 5);
    }

    void FixedUpdate()
    {
        player_insight = Physics.CheckSphere(transform.position, sightRange, whatsPlayer);
        player_inattack = Physics.CheckSphere(transform.position, attackRange, whatsPlayer);

        if (destination_reached())
        {
            walk_set = false;
        }

        if (walk_set == false)
        {
            animator.SetTrigger("idle");
        }
        else
        {
            animator.SetTrigger("walk");
        }

        if (!player_insight && !player_inattack) wander();
        if ((player_insight && !player_inattack) || damage_taken) chase();
        if (player_insight && player_inattack) attack();
    }


    //IF AN ENEMY IS WANDERING/////////////////////////////////////////////
    private void wander()
    {
        sounds.stop_chase_noise();
        if (chased == true)
        {
            walk_set = false;
            chased = false;
            enemy.isStopped = true;
        }
        if (!walk_set)
        {
            if (cr_running == false)
            {
                StartCoroutine(walk_delay_set());
            }
        }

        IEnumerator walk_delay_set()
        {
                cr_running = true;
                yield return new WaitForSeconds(walk_delay);
                if (enemy.enabled == true)
                {
                    walk_set = true;
                    enemy.speed = walk_speed;
                    enemy.isStopped = false;
                    setpoint();
                    cr_running = false;
                }
        }

    }


    //IF AN ENEMY IS CHASING/////////////////////////////////////////////
    private void chase()
    {
        if(chased == false)
        {
            Debug.Log("CALLED");
            sounds.play_chase_noise();
        }
        chased = true;
        walk_set = true;
        enemy.isStopped = false;
        enemy.speed = chase_speed;
        enemy.SetDestination(player.position);
    }

    //IF AN ENEMY ATTACKS////////////////////////////////////////////////
    private void attack()
    {
        //stop enemy
        
        enemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!has_attacked)
        {
            player.gameObject.GetComponent<FirstPersonAIO>().enemy_hit_player(0.2f, 0.15f, transform.forward);
            animator.SetTrigger("attack");
            sounds.play_attack_noise();
            has_attacked = true;
            player.gameObject.GetComponent<player_inventory>().health = player.gameObject.GetComponent<player_inventory>().health - 50;
            Invoke(nameof(ResetAttack), attack_delay);
        }
    }



    //BASIC METHODS/////////////////////////////////////////////////////
    private void ResetAttack()
    {
        has_attacked = false;
    }

    public void setpoint()
    {
        if (enemy.enabled == true)
        {
            walk_point = RandomNavmeshLocation(15f);
            enemy.SetDestination(walk_point);
        }   
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
        if (distanceToTarget <= .5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
