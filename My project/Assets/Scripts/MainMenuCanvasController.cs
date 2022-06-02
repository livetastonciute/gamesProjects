using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainMenu;

    [SerializeField]
    private RectTransform settingsMenu;

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

    public void ShowMainMenu()
    {
        Hide(settingsMenu);
        Show(mainMenu);
    }

    public void ShowSettingsMenu()
    {
        Hide(mainMenu);
        Show(settingsMenu);
    }
    void Start()
    {
        ShowMainMenu();
        UpdateSliders();
    }

    public void StartGame()
    {
        //įprastai  Scenes.LoadNextScene();
        //žemiau pateikiamas su smooth transition
        if (changingScenes)
        {
            return;
        }
        StartCoroutine(StartGameDelayed());
    }

    public void ExitGame()
    {
        //įprastai Scenes.ExitGame();
        //žemiau pateikiamas su smooth transition
        StartCoroutine(ExitGameDelayed());
    }

    public void UpdateSliders()
    {
        musicVolumeSlider.SetValueWithoutNotify(mixerController.MusicVolume);
        effectsVolumeSlider.SetValueWithoutNotify(mixerController.EffectsVolume);
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
}
