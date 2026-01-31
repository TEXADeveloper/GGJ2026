using UnityEngine;

public class PickableObject : InteractiveObject
{
    public override void Interact(PlayerController playerController)
    {
        if (playerController.PickUpObject(this))
            Destroy(this.gameObject);
    }
}
