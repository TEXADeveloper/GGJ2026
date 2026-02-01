using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static event Action<bool> HasMask;
    public static event Action canEscape;

    [SerializeField] private ObjectDisplay display;
    [SerializeField] private JumpScare jumpScare;
    [SerializeField] private Animator anim;
    [SerializeField] public bool canLeave = false;

    [Header("Mask Functionality")]
    [SerializeField] private ScriptableRendererFeature xrayFeature;
    private bool hasMaskOn = false;

    [Header("Objects")]
    [SerializeField] private Transform objectParent;
    [SerializeField] private List<PickableObject> pickableObjects;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pickableObjects = objectParent.GetComponentsInChildren<PickableObject>().ToList<PickableObject>();
        display.SetMax(pickableObjects.Count);
    }

    public bool PickupMask()
    {
        if (hasMaskOn)
            return false;
        
        hasMaskOn = true;
        xrayFeature.SetActive(true);
        HasMask?.Invoke(true);
        return true;
    }

    public bool PickUpObject(PickableObject pickable)
    {
        if (pickableObjects.Contains(pickable))
        {
            pickableObjects.Remove(pickable);
            display.PickObject();

            if (pickableObjects.Count <= 0)
            {
                canLeave = true;
                canEscape?.Invoke();
            }
            return true;
        }
        return false;
    }

    public void Hurt()
    {
        if (hasMaskOn)
        {
            hasMaskOn = false;
            xrayFeature.SetActive(false);
            HasMask?.Invoke(false);
            anim.SetTrigger("Shake");
            return;
        }
        jumpScare.LoseGame();
    }

    void OnDestroy()
    {
        xrayFeature.SetActive(false);
    }
}
