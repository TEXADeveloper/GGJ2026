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
}
