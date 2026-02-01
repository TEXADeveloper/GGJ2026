using UnityEngine;

public class InteractionMessage : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void OnEnable()
    {
        InteractiveObject.InsideInteractionArea += insideArea;
    }

    void OnDisable()
    {
        InteractiveObject.InsideInteractionArea -= insideArea;
    }

    private void insideArea(bool inside)
    {
        anim.SetBool("Inside", inside);
    }
}
