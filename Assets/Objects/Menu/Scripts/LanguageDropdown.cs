using UnityEngine;
using TMPro;

public class LanguageDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int lastValue = -1;

    

    void Update()
    {
        if (dropdown.value == lastValue) return;

        lastValue = dropdown.value;

        if (dropdown.value == 0)
        {
            LanguageManager.Instance.SetLanguage(0);
        }
        else if (dropdown.value == 1)
        {
            LanguageManager.Instance.SetLanguage(1);
        }
    }
}
