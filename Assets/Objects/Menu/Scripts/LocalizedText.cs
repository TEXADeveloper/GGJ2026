using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string spanishText;
    public string englishText;

    TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        if (LanguageManager.Instance.currentLanguage == Language.Spanish)
            textMesh.text = spanishText;
        else
            textMesh.text = englishText;
    }
}
