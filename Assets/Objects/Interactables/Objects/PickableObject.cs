using UnityEngine;

public class PickableObject : InteractiveObject
{
    [SerializeField] private Light lightComponent;

    void OnEnable()
    {
        PlayerController.HasMask += setLight;
    }

    void OnDisable()
    {
        PlayerController.HasMask -= setLight;
    }

    private void setLight(bool light)
    {
        lightComponent.enabled = light;
    }

    public override void Interact(PlayerController playerController)
    {
        if (playerController.PickUpObject(this))
            Destroy(this.gameObject);
    }
}
