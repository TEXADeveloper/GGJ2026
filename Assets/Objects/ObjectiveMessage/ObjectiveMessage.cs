using Unity.VisualScripting;
using UnityEngine;

public class ObjectiveMessage : MonoBehaviour
{
    [SerializeField] InputController input;
    
    public void EnableInput()
    {
        input.enabled = true;
    }

    public void DestroyItself()
    {
        Destroy(this.gameObject);
    }
}
