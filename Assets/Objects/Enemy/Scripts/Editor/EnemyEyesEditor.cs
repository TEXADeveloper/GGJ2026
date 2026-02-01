using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyEyes))]
public class EnemyEyesEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyEyes fov = (EnemyEyes)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.maxDistance);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.maxDistance);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.maxDistance);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerCollider.transform.position + Vector3.up);
        }
        if (fov.canSeeLight)
        {
            Handles.color = Color.yellow;
            Handles.DrawLine(fov.transform.position, fov.lightCollider.transform.position);
        }

        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.proximityRealization);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}