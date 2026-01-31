using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractiveObject : MonoBehaviour
{
    public abstract void Interact(PlayerController playerController);

    public virtual void OnTriggerEnter(Collider other) 
    {
        other.GetComponent<PlayerInteraction>().SetInteractiveObject(this);
    }

    public virtual void OnTriggerExit(Collider other) 
    {
        other.GetComponent<PlayerInteraction>().SetInteractiveObject(null);
    }
}
