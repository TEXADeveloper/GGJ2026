using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
   // [Header("Resolution")]
    //public TMP_Dropdown resolutionDropdown;

    [SerializeField] private VolumeProfile volume;
    [SerializeField] private Slider brightnessSlider;
    private LiftGammaGain gamma;
    float gammaValue = 0;

    Resolution[] resolutions;

    void Start()
    {
        loadResolutions();
    }

    void OnEnable()
    {
        volume.TryGet(out gamma);
        UpdateGamma(PlayerPrefs.GetFloat("Gamma", gamma.gamma.value.w));
        brightnessSlider.value = gammaValue;
    }

    private void loadResolutions()
    {
        resolutions = Screen.resolutions;

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
    }

    // ---------- AUDIO ----------

    // ---------- RESOLUTION ----------
    /*public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }*/

    // ---------- LANGUAGE (BASE SIMPLE) ----------

    public void UpdateGamma(float newGamma)
    {
        gammaValue = newGamma;
        PlayerPrefs.SetFloat("Gamma", gammaValue);
        PlayerPrefs.Save();

        gamma.gamma.Override(new Vector4(1f, 1f, 1f, gammaValue));
    }
}
