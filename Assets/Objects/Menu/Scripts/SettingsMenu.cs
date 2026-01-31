using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;

   // [Header("Resolution")]
    //public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        //resolutions = Screen.resolutions;
        //resolutionDropdown.ClearOptions();

        int currentRes = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

       /* resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentRes;
        resolutionDropdown.RefreshShownValue();*/
    }

    // ---------- AUDIO ----------
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    // ---------- RESOLUTION ----------
    /*public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }*/

    // ---------- LANGUAGE (BASE SIMPLE) ----------
    
}
