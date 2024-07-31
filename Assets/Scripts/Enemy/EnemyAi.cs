using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private Enemy enemy;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    bool isalreadyAttacked = false;

    // States
    public float sightRange, attackRange;
    public bool isPlayerInSightRange, isPlayerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        agent.speed = enemy.moveSpeed;
    }

    private void Update()
    {
        // Check for sight and attck range
        isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!isPlayerInSightRange && !isPlayerInAttackRange && !enemy.isDeath) Patroling();
        if (isPlayerInSightRange && !isPlayerInAttackRange && !enemy.isDeath) ChasePlayer();
        if (isPlayerInSightRange && isPlayerInAttackRange && !enemy.isDeath) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            enemy.Walking();
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reach
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;


    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        enemy.Walking();
    }

    private void AttackPlayer()
    {
        // Make enemy not move when attack
        agent.SetDestination(transform.position);

        if (isalreadyAttacked == false)
        {
            Debug.Log("Attack");
            transform.LookAt(player);

            // Attack
            enemy.Invoke(nameof(enemy.Attack), enemy.timeBeforeAttack);

            isalreadyAttacked = true;
            Invoke(nameof(ResetAttack), enemy.timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        isalreadyAttacked = false;
    }

}
