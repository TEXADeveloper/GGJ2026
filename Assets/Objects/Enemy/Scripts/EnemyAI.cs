using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyEyes eyes;
    [SerializeField] private AK.Wwise.RTPC distanceParameter;
    [SerializeField] private SoundTrigger soundTrigger;
    [SerializeField] private float speed;
    private Transform currentTarget;

    [Header("Patrol")]
    [SerializeField] private Transform patrolPointsParent;
    [SerializeField, Range(0f, 2f)] private float minDistanceToTarget;
    [SerializeField, Range(0f, 10f)] private float minWaitingTime;
    [SerializeField, Range(0f, 10f)] private float maxWaitingTime;
    private List<Transform> patrolPoints;
    private float waitTimer;

    [Header("Follow Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField, Range(0f, 2f)] private float hurtDistance;
    [SerializeField, Range(0f, 10f)] private float stunTime;
    [SerializeField, Range(0f, 5f)] private float keepFollowingTime;
    private bool isFollowingPlayer = false;
    private bool playerLostTimer = false;
    private bool enemyStunned = false;
    private float stunTimer;

    [Header("Following Light")]
    private bool isFollowingLight = false;
    private Vector3 lightPosition;

    [Header("PlayerPicked Increments")]
    [SerializeField, Range(-1f, 1f)] private float speedIncrement;
    [SerializeField, Range(-1f, 1f)] private float stunIncrement;
    [SerializeField, Range(-1f, 1f)] private float minWaitingIncrement;
    [SerializeField, Range(-1f, 1f)] private float maxWaitingIncrement;

    void OnEnable()
    {
        PlayerController.PickObjects += playerPicked;

        agent.speed = speed;

        patrolPoints = patrolPointsParent.GetComponentsInChildren<Transform>().ToList();
        patrolPoints.Remove(patrolPointsParent.transform);

        currentTarget = patrolPoints[Random.Range(0, patrolPoints.Count)];
        waitTimer = 0f;
    }

    void OnDisable()
    {
        PlayerController.PickObjects -= playerPicked;
        SoundSingleton.instance.SetMaxDistance();
    }

    void Start()
    {
        soundTrigger.PlaySound("Hover");
    }

    private void playerPicked(bool picked)
    {
        if (picked)
        {
            speed += speedIncrement;
            stunTime += stunIncrement;
            minWaitingTime += minWaitingIncrement;
            maxWaitingTime += maxWaitingIncrement;
        } else
        {
            speed -= speedIncrement;
            stunTime -= stunIncrement;
            minWaitingTime -= minWaitingIncrement;
            maxWaitingTime -= maxWaitingIncrement;
        }

        agent.speed = speed;
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

        float distanceToPlayer = distanceToTarget(transform.position, playerTransform.position);
        distanceParameter.SetGlobalValue(distanceToPlayer);
    }

    void FixedUpdate()
    {
        anim.SetBool("Moving", agent.velocity.magnitude > 0.15f);
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
                waitTimer = keepFollowingTime;
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
            anim.SetTrigger("Attack");

            enemyStunned = true;
            stunTimer = stunTime;
        }
    }

    private void followLight()
    {
        if (!isFollowingLight)
        {
            currentTarget = eyes.lightCollider.transform;

            waitTimer = Random.Range(minWaitingTime, maxWaitingTime);

            isFollowingLight = true;
        } else if (eyes.canSeeLight)
        {
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
            lightPosition = currentTarget.position;
            playerLostTimer = false;
        }

        if (distanceToTarget(transform.position, lightPosition) <= minDistanceToTarget)
        {
            agent.isStopped = true;
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                isFollowingLight = false;
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
                newTarget = patrolPoints[Random.Range(0, patrolPoints.Count)];
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
