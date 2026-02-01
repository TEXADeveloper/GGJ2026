using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    [SerializeField] private SoundTrigger trigger;
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
        if (lightEnabled)
        {
            trigger.PlaySound("FlashOn");
        }
        else
        {
            trigger.PlaySound("FlashOff");
        }
    }

    void LateUpdate()
    {
        if (!lightEnabled)
            return;

        RaycastHit hit;
        if (Physics.Raycast(spotLight.position, spotLight.forward, out hit, maxDistance, mask))
            lightCollider.position  = hit.point - spotLight.forward;
    }
}
