using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Mask Functionality")]
    [SerializeField] private ScriptableRendererFeature xrayFeature;
    private bool hasMaskOn = false;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool PickupMask()
    {
        if (hasMaskOn)
            return false;
        
        hasMaskOn = true;
        xrayFeature.SetActive(true);
        return true;
    }

    
    public void Hurt()
    {
        if (hasMaskOn)
        {
            Debug.Log("MaskDestroyed");
            hasMaskOn = false;
            xrayFeature.SetActive(false);
            return;
        }
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
