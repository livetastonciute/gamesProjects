using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainMenu;

    [SerializeField]
    private RectTransform optionsMenu;

    [SerializeField]
    private Slider musicVolumeSlider;

    [SerializeField]
    private Slider effectsVolumeSlider;

    [SerializeField]
    private MixerController mixerController;

    private SceneFader sceneFader;
    private bool changingScenes;

    private void Awake()
    {
        sceneFader = GetComponent<SceneFader>();
    }

    private static void Show(Component component)
    {
        component.gameObject.SetActive(true);
    }

    private static void Hide(Component component)
    {
        component.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    private void Start()
    {
        ShowMainMenu();
        UpdateSliders();
    }

    private void UpdateSliders()
    {
        musicVolumeSlider.SetValueWithoutNotify(mixerController.MusicVolume);
        effectsVolumeSlider.SetValueWithoutNotify(mixerController.EffectsVolume);
    }
    public void StartGame()
    {
        if (changingScenes)
        {
            return;
        }

        StartCoroutine(StartGameDelayed());
    }

    public void ExitGame()
    {
        if (changingScenes)
        {
            return;
        }

        StartCoroutine(ExitGameDelayed());

    }

    private IEnumerator StartGameDelayed()
    {
        changingScenes = true;
        yield return sceneFader.FadeOutScene();
        Scenes.LoadNextScene();
    }

    private IEnumerator ExitGameDelayed()
    {
        changingScenes = true;
        yield return sceneFader.FadeOutScene();
        Scenes.ExitGame();
    }

    public void ShowMainMenu()
    {
        Show(mainMenu);
        Hide(optionsMenu);
    }

    public void ShowOptionsMenu()
    {
        Hide(mainMenu);
        Show(optionsMenu);
    }
}
