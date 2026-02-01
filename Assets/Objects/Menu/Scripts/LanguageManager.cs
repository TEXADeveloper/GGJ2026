using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public void SetLanguage(int index)
    {
        PlayerPrefs.SetInt("Language", index);
        PlayerPrefs.Save();

        // Actualiza solo los que están visibles ahora mismo.
        // Los que están desactivados se actualizarán solos al activarse gracias al OnEnable.
        LocalizedText[] activeTexts = FindObjectsByType<LocalizedText>(FindObjectsSortMode.None);
        foreach (var text in activeTexts)
        {
            text.UpdateText();
        }
    }
}