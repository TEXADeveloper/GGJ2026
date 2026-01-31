using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement pM;

    void Start()
    {
        pM = this.GetComponent<PlayerMovement>();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
