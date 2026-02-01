using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string spanishText;
    public string englishText;
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Se ejecuta cada vez que el objeto pasa de estar desactivado a activado
    void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (textMesh == null) textMesh = GetComponent<TextMeshProUGUI>();
        if (textMesh == null) return;

        int langIndex = PlayerPrefs.GetInt("Language", 0);
        Language currentLanguage = (Language)langIndex;

        textMesh.text = (currentLanguage == Language.Spanish) ? spanishText : englishText;
    }
}