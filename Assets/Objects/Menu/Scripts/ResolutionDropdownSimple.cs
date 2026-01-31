using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResolutionDropdownSimple : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>()
        {
            "1280 x 720",
            "1366 x 768 (Laptop)",
            "1600 x 900",
            "1920 x 1080",
            "2560 x 1440"
        };

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    void ChangeResolution(int index)
    {
        if (index == 0)
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
        else if (index == 1)
        {
            Screen.SetResolution(1366, 768, Screen.fullScreen);
        }
        else if (index == 2)
        {
            Screen.SetResolution(1600, 900, Screen.fullScreen);
        }
        else if (index == 3)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
        else if (index == 4)
        {
            Screen.SetResolution(2560, 1440, Screen.fullScreen);
        }
    }
}
