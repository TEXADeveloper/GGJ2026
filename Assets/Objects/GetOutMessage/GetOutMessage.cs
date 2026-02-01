using UnityEngine;

public class GetOutMessage : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void OnEnable()
    {
        PlayerController.canEscape += escape;
    }

    void OnDisable()
    {
        PlayerController.canEscape -= escape;
    }

    private void escape()
    {
        anim.SetBool("Inside", true);
    }
}
