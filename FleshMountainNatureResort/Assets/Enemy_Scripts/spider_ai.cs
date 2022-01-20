using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class spider_ai : MonoBehaviour
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
    }

    void FixedUpdate()
    {
        player_inattack = Physics.CheckSphere(transform.position, attackRange, whatsPlayer);

        if (!player_inattack && !animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|crab_startle")) chase();
        if (player_inattack) attack();
    }


    //IF AN ENEMY IS CHASING/////////////////////////////////////////////
    private void chase()
    {
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
            player.gameObject.GetComponent<FirstPersonAIO>().enemy_hit_player(0.1f, 0.07f, transform.forward);
            animator.SetTrigger("attack");
            has_attacked = true;
            player.gameObject.GetComponent<player_inventory>().health = player.gameObject.GetComponent<player_inventory>().health - 5;
            Invoke(nameof(ResetAttack), attack_delay);
        }
    }



    //BASIC METHODS/////////////////////////////////////////////////////
    private void ResetAttack()
    {
        has_attacked = false;
    }

}
