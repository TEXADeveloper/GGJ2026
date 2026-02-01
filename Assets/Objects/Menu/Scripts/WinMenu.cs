using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinMenu : MonoBehaviour
{
    public enum GameResult { Lose = 0, Win = 1 }

    [Header("Config")]
    public GameResult result;
    public LocalizedText titleLocalized; // El LocalizedText del t�tulo Win/Lose

    [Header("Strings")]
    public string winES = "�VICTORIA!";
    public string winEN = "VICTORY!";
    public string loseES = "DERROTA";
    public string loseEN = "DEFEAT";
    [Header("Imagen")]
    public Image _background;
    public Sprite _backgroundWin;
    public Sprite _backgroundLose;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeDuration = 1f;
    public string gameSceneName = "Game";
    public GameObject _fade;
    void Start()
    {
        // Leemos el resultado (puedes setearlo antes de cargar la escena)
        //result = (GameResult)PlayerPrefs.GetInt("GameResult", 0);

        ApplyTexts();
        StartCoroutine(FadeIn());
        _fade.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ApplyTexts()
    {
        if (result == GameResult.Win)
        {
            titleLocalized.spanishText = winES;
            titleLocalized.englishText = winEN;
            _background.sprite = _backgroundWin;
        }
        if (result == GameResult.Lose)
        {
            titleLocalized.spanishText = loseES;
            titleLocalized.englishText = loseEN;
            _background.sprite= _backgroundLose;
        }

        titleLocalized.UpdateText();
    }
    IEnumerator FadeIn()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
        _fade.SetActive(false);
    }
    public void PlayGame() => StartCoroutine(FadeOutAndLoad());
    IEnumerator FadeOutAndLoad()
    {
        _fade.SetActive(true);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
        SceneManager.LoadScene(0);
    }
}