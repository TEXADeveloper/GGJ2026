using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public Language currentLanguage;
    public TMP_Dropdown languageDropdown;

    [Header("Texts assigned from Inspector")]
    public List<LocalizedText> localizedTexts = new List<LocalizedText>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguage();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        

        UpdateAllTexts(); // fuerza idioma al iniciar
    }

    void LoadLanguage()
    {
        int lang = PlayerPrefs.GetInt("Language", 0);
        currentLanguage = (Language)lang;
    }

    public void SetLanguage(int index)
    {
        Debug.Log("Idioma cambiado a: " + index);
        currentLanguage = (Language)index;
        PlayerPrefs.SetInt("Language", index);
        UpdateAllTexts();
    }

    void UpdateAllTexts()
    {
        foreach (var text in localizedTexts)
        {
            if (text != null)
                text.UpdateText();
        }
    }
}
