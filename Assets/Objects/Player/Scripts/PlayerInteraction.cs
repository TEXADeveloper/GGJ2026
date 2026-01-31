using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private InteractiveObject currentInteractive = null;

    public void SetInteractiveObject(InteractiveObject newObject)
    {
        currentInteractive = newObject;
    }

    public void Interact()
    {
        if (currentInteractive != null)
        {
            currentInteractive.Interact(playerController);
        }
    }
}
