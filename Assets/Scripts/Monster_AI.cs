using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster_AI : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;
    public float chaseDistance = 10f; 
    public float attackDistance = 4f;
    public float timeBetweenAttacks = 2f;
    public float moveSpeed = 3f;

    private NavMeshAgent Agent;
    public int currentPatrolIndex = 0;
    bool isPatroling = false;
    bool isAttacking = false;
    private bool textShow;
    public static bool HavAccessSeeplayer = true;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance && HavAccessSeeplayer)
        {
            isPatroling = false;

            if (!isAttacking)
            {
                isAttacking = true;
                textShow = false;
                StartCoroutine(AttackAndWait());
            }
        }
        else if (distanceToPlayer <= chaseDistance && HavAccessSeeplayer)
        {
            isPatroling = false;
            ChasePlayer();
        }
        else if (!isPatroling)
        {
            PatrolToNextPoint();
        }
    }

    private IEnumerator AttackAndWait()
    {
        if (!textShow)
        {
            textShow = true;
            UIController.instance.ShowText("The monster is attacking you");
        }
        Agent.isStopped = true;
        Attack();

        yield return new WaitForSeconds(timeBetweenAttacks);

        isAttacking = false;
    }

    public static void Attack()
    {
        if (!PlayerMovment.Dead)
        {
            PlayerMovment.Health -= 5;
        }
    }
    private void ChasePlayer()
    {
        if (!textShow)
        {
            textShow = true;
            UIController.instance.ShowText("The monster found you,Ruuun");
        }
        Agent.isStopped = false;
        Agent.SetDestination(player.position);
    }

    private void PatrolToNextPoint()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }
        isPatroling = true;
        textShow = false;
        Agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        NavMeshPath path = new NavMeshPath();
        if (!Agent.CalculatePath(patrolPoints[currentPatrolIndex].position, path) || path.status == NavMeshPathStatus.PathPartial)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            PatrolToNextPoint();
            return;
        }

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PatrolPoint"))
        {
            PatrolToNextPoint();
        }
    }
}
