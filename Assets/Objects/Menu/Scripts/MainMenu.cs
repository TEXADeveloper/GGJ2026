using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("Fade")]
    public Image fadeImage;
    public float fadeDuration = 1f;
    public string gameSceneName = "Game";
    public GameObject _fade;

    [Header("Panels")]
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    [Header("Menu Base")]
    public GameObject _main;

    [Header("Animation Settings")]
    public float animationDuration = 0.2f;

    // Variables privadas para guardar tus escalas personalizadas del editor
    private Vector3 _creditsOriginalScale;
    private Vector3 _settingsOriginalScale;

    void Awake()
    {
        // Guardamos la escala que t� configuraste en el Inspector antes de hacer nada
        _creditsOriginalScale = creditsPanel.transform.localScale;
        _settingsOriginalScale = settingsPanel.transform.localScale;
    }

    void Start()
    {
        // Ponemos escala 0 para que empiecen invisibles
        creditsPanel.transform.localScale = Vector3.zero;
        settingsPanel.transform.localScale = Vector3.zero;

        creditsPanel.SetActive(false);
        settingsPanel.SetActive(false);

        StartCoroutine(FadeIn());
        _fade.SetActive(true);
        _main.SetActive(true);
    }

    // ---------- L�GICA DE ESCALADO CORREGIDA ----------
    IEnumerator ScalePanel(GameObject panel, Vector3 targetScale, bool deactivateAtEnd)
    {
        float timer = 0;
        Vector3 startScale = panel.transform.localScale;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;
            // Usamos una curva suave
            panel.transform.localScale = Vector3.Lerp(startScale, targetScale, Mathf.SmoothStep(0, 1, progress));
            yield return null;
        }

        panel.transform.localScale = targetScale;
        if (deactivateAtEnd) panel.SetActive(false);
    }

    // ---------- CREDITS ----------
    public void OpenCredits()
    {
        _main.SetActive(false);
        creditsPanel.SetActive(true);
        StopCoroutine("ScalePanel"); // Detenemos solo la animaci�n para no romper el Fade
        StartCoroutine(ScalePanel(creditsPanel, _creditsOriginalScale, false));
    }

    public void CloseCredits()
    {
        _main.SetActive(true);
        StartCoroutine(ScalePanel(creditsPanel, Vector3.zero, true));
    }

    // ---------- SETTINGS ----------
    public void OpenSettings()
    {
        _main.SetActive(false);
        settingsPanel.SetActive(true);
        StopCoroutine("ScalePanel");
        StartCoroutine(ScalePanel(settingsPanel, _settingsOriginalScale, false));
    }

    public void CloseSettings()
    {
        _main.SetActive(true);
        StartCoroutine(ScalePanel(settingsPanel, Vector3.zero, true));
    }

    // M�todos de Fade y Play (se mantienen igual)
    public void PlayGame() => StartCoroutine(FadeOutAndLoad());

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void QuitGame() => Application.Quit();
}