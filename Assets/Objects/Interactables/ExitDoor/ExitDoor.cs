using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : InteractiveObject
{
    public override void Interact(PlayerController playerController)
    {
        if (playerController.canLeave)
        {
            SceneManager.LoadScene("Scenes/Win condition");
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        PlayerController pC = other.GetComponent<PlayerController>();

        if (pC == null || !pC.canLeave)
            return;

        other.GetComponent<PlayerInteraction>().SetInteractiveObject(this);
        callInteractionArea(true);
        inside = true;
    }
}
