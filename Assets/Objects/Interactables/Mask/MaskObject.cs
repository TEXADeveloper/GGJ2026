using UnityEngine;

public class MaskObject : InteractiveObject
{
    public override void Interact(PlayerController playerController)
    {
        if (playerController.PickupMask())
            Destroy(this.gameObject);
    }
}
