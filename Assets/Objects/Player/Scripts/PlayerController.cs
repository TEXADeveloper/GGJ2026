using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Mask Functionality")]
    [SerializeField] private ScriptableRendererFeature xrayFeature;
    [SerializeField] private GameObject UIMask;
    private bool hasMaskOn = false;

    [Header("Objects")]
    [SerializeField] private Transform objectParent;
    [SerializeField] private List<PickableObject> pickableObjects;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

        pickableObjects = objectParent.GetComponentsInChildren<PickableObject>().ToList<PickableObject>();
    }

    public bool PickupMask()
    {
        if (hasMaskOn)
            return false;
        
        hasMaskOn = true;
        xrayFeature.SetActive(true);
        UIMask.SetActive(true);
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
            UIMask.SetActive(false);
            return;
        }
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
