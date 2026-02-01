using UnityEngine;
using TMPro;

public class LanguageDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public LanguageManager languageManager; // Arrastra el objeto con el LanguageManager aquí

    void Start()
    {
        // Cargamos el valor guardado al iniciar el menú
        if (dropdown != null)
        {
            dropdown.value = PlayerPrefs.GetInt("Language", 0);

            // Suscribirse al evento para no usar Update
            dropdown.onValueChanged.AddListener(OnDropdownChanged);
        }
    }

    void OnDropdownChanged(int index)
    {
        if (languageManager != null)
        {
            languageManager.SetLanguage(index);
        }
        else
        {
            Debug.LogError("¡Falta el LanguageManager en el Inspector de este Dropdown!");
        }
    }
}