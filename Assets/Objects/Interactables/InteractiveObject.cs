using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractiveObject : MonoBehaviour
{
    public static event Action<bool> InsideInteractionArea;
    public bool inside = false;

    public abstract void Interact(PlayerController playerController);

    public virtual void OnTriggerEnter(Collider other) 
    {
        other.GetComponent<PlayerInteraction>().SetInteractiveObject(this);
        callInteractionArea(true);
        inside = true;
    }

    public virtual void OnTriggerExit(Collider other) 
    {
        other.GetComponent<PlayerInteraction>().SetInteractiveObject(null);
        callInteractionArea(false);
        inside = false;
    }

    public void callInteractionArea(bool value)
    {
        InsideInteractionArea?.Invoke(value);
    }

    void OnDestroy()
    {
        if (inside)
        {
            InsideInteractionArea?.Invoke(false);
        }
    }
}
