using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyAI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyEyes eyes;
    private Transform currentTarget;
    NavMeshPath path;

    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField, Range(0f, 2f)] private float minDistanceToTarget;
    [SerializeField, Range(0f, 10f)] private float minWaitingTime;
    [SerializeField, Range(0f, 10f)] private float maxWaitingTime;
    private float waitTimer;

    [Header("Follow Player")]
    [SerializeField, Range(0f, 2f)] private float hurtDistance;
    [SerializeField, Range(0f, 10f)] private float stunTime;
    private bool isFollowingPlayer = false;
    private bool playerLostTimer = false;
    private bool enemyStunned = false;
    private float stunTimer;

    [Header("Following Light")]
    private bool isFollowingLight = false;

    void Start()
    {
        path = agent.path;
        currentTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
    }

    void Update()
    {
        if (enemyStunned)
        {
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0)
            {
                enemyStunned = false;
                stunTimer = 0;
            }
        }
    }

    void LateUpdate()
    {
        if (!enemyStunned && (eyes.canSeePlayer || isFollowingPlayer))
        {
            followPlayer();
        }
        else if (!enemyStunned && (eyes.canSeeLight || isFollowingLight))
        {
            followLight();
        }
        else
        {
            patrol();
        }
    }

    private void followPlayer()
    {
        if (!isFollowingPlayer)
        {
            currentTarget = eyes.playerCollider.transform;

            agent.isStopped = false;

            isFollowingPlayer = true;
            isFollowingLight = false;
        } else if (eyes.canSeePlayer)
        {
            agent.SetDestination(currentTarget.position);
            playerLostTimer = false;
        }
        else
        {
            //? is following though it cannot see him
            if (!playerLostTimer)
            {
                waitTimer = Random.Range(minWaitingTime, maxWaitingTime);
                playerLostTimer = true;
            }
            else
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    playerLostTimer = false;
                    isFollowingPlayer = false;
                }
            }
        }

        if (distanceToTarget(transform.position, currentTarget.position) <= hurtDistance)
        {
            eyes.playerCollider.GetComponent<PlayerController>().Hurt();

            enemyStunned = true;
            stunTimer = stunTime;
        }
    }

    private void followLight()
    {
        if (!isFollowingLight)
        {
            currentTarget = eyes.lightCollider.transform;

            agent.SetDestination(currentTarget.position);
            agent.isStopped = false;

            waitTimer = Random.Range(minWaitingTime, maxWaitingTime);

            isFollowingLight = true;
        }
        else
        {
            if (distanceToTarget(transform.position, currentTarget.position) <= minDistanceToTarget)
            {
                agent.isStopped = true;
                waitTimer -= Time.deltaTime;
                if (waitTimer < 0)
                {
                    isFollowingLight = false;
                }
            }
        }
    }

    private void patrol()
    {
        //*  Follow points Randomly
        if (currentTarget == null)
            return;
        if (waitTimer <= 0)
        {
            //? Set new Objective
            Transform newTarget = null;
            while (newTarget == null || newTarget == currentTarget)
            {
                newTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
            }
            currentTarget = newTarget;

            //? Start moving

            agent.SetDestination(currentTarget.position);
            agent.isStopped = false;

            //? Set Timer
            waitTimer = Random.Range(minWaitingTime, maxWaitingTime);
        }
        else
        {
            //? Stop Moving
            if (distanceToTarget(transform.position, currentTarget.position) <= minDistanceToTarget)
            {
                agent.isStopped = true;
                waitTimer -= Time.deltaTime;
            }
        }
    }

    private float distanceToTarget(Vector3 from, Vector3 to)
    {
        Vector3 horizontalFrom = from - from.y * Vector3.up;
        Vector3 horizontalTo = to - to.y * Vector3.up;

        return Vector3.Distance(horizontalFrom, horizontalTo);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, hurtDistance);
    }
}
