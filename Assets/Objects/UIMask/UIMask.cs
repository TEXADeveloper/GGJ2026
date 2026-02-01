using UnityEngine;

public class UIMask : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void OnEnable()
    {
        PlayerController.HasMask += setTrigger;
    }

    void OnDisable()
    {
        PlayerController.HasMask -= setTrigger;
    }

    private void setTrigger(bool value)
    {
        anim.SetBool("Mask", value);
    }
}
