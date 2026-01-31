using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    [SerializeField] private Transform spotLight;
    [SerializeField] private Transform lightCollider;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask mask;
    private bool lightEnabled;

    void Start()
    {
        lightEnabled = spotLight.gameObject.activeSelf;
    }

    public void Toggle()
    {
        lightEnabled = !lightEnabled;
        spotLight.gameObject.SetActive(lightEnabled);
        lightCollider.gameObject.SetActive(lightEnabled);
    }

    void LateUpdate()
    {
        if (!lightEnabled)
            return;

        RaycastHit hit;
        if (Physics.Raycast(spotLight.position, spotLight.forward, out hit, maxDistance, mask))
            lightCollider.position  = hit.point;
    }
}
