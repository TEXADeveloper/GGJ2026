using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        return true;
    }

    
    public void Hurt()
    {
        if (hasMaskOn)
        {
            Debug.Log("MaskDestroyed");
            hasMaskOn = false;
            return;
        }
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
