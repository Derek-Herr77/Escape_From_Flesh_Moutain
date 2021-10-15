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
    public float walk_point_range;
    public float walk_delay;

    //attack
    public float attack_delay;
    bool has_attacked;

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
    void Update()
    {
        player_insight = Physics.CheckSphere(transform.position, sightRange, whatsPlayer);
        player_inattack = Physics.CheckSphere(transform.position, attackRange, whatsPlayer);

        if (!player_insight && !player_inattack) wander();
        if (player_insight && !player_inattack) chase();
        if (player_insight && player_inattack) attack();

        if(enemy.transform.position == walk_point)
        {
            walk_set = false;
        }
    }

    private void wander()
    {
        if (!walk_set)
        {
            animator.SetBool("seen", false);
            Invoke(nameof(find_walkpoint), walk_delay);
        }
    }

    private void find_walkpoint()
    {
        float randomZ = Random.Range(-walk_point_range, walk_point_range);
        float randomX = Random.Range(-walk_point_range, walk_point_range);

        walk_point = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walk_point, -transform.up, 2f, whatsGround))
        {
            walk_set = true;
            animator.SetBool("seen", true);
            enemy.SetDestination(walk_point);
        }
    }

    private void chase()
    {
        walk_set = false;
        animator.SetBool("seen", true);
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

            Invoke(nameof(ResetAttack), attack_delay);
        }
    }

    private void ResetAttack()
    {
        has_attacked = false;
    }

    void hitbyray()
    {
        StartCoroutine(turnoff());
        IEnumerator turnoff()
        {
            enemy.isStopped = true;
            yield return new WaitForSeconds(0.5f);
            enemy.ResetPath();
         

        }
    }

    
}
