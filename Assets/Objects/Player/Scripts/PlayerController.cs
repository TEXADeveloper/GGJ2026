using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public static event Action<bool> HasMask;

    [SerializeField] private Animator anim;

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

            if (pickableObjects.Count <= 0)
            {
                Debug.Log("Ya puedes Escapar");
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
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    void OnDestroy()
    {
        xrayFeature.SetActive(false);
    }
}
