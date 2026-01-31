using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class EnemyEyes : MonoBehaviour
{
    [Header("General")]
    [SerializeField, Range(0f, 360f)] public float angle;
    [SerializeField] public float maxDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;
    
    [Header("Player")]
    [SerializeField] public Collider playerCollider;
    [HideInInspector] public bool canSeePlayer = false;

    [Header("Light")]
    [SerializeField] public Collider lightCollider;
    [HideInInspector] public bool canSeeLight = false;

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxDistance, playerLayer);

        if (colliders.Length <= 0)
        {
            if (canSeePlayer || canSeeLight)
            {
                canSeePlayer = false;
                canSeeLight = false;
            }
            return;
        }

        if (colliders.Contains(playerCollider))
        {
            Vector3 directionToTarget = (playerCollider.transform.position + Vector3.up - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, playerCollider.transform.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    canSeePlayer = true;
                    canSeeLight = false;
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        if (!canSeePlayer && colliders.Contains(lightCollider))
        {
            Vector3 directionToTarget = (lightCollider.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, lightCollider.transform.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                    canSeeLight = true;
                else
                    canSeeLight = false;
            }
            else
                canSeeLight = false;
        }
    }
}
