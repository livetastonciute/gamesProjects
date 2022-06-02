using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    private RectTransform pausePanel;

    [SerializeField]
    private RectTransform gameOverPanel;

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
    void Start()
    {
        Hide(pausePanel);
        Hide(gameOverPanel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    public void ShowPauseMenu()
    {
        Show(pausePanel);
    }

    public void ExitGame()
    {
        //Įprastai  Scenes.ExitGame();
        //Žemiau su smooth

        if (changingScenes)
        {
            return;
        }
        StartCoroutine(ExitGameDelayed());
    }

    public void ResumeGame()
    {
        Hide(pausePanel);
    }

    public void GoToMenu() 
    {
        Scenes.LoadPreviousScene();
    }

    public void RestartGame()
    {
        //Įprastai Scenes.RestartScene();
        //Žemiau su smooth

        if (changingScenes)
        {
            return;
        }
        StartCoroutine(RestartSceneDelayed());
    }

    public void GameOver()
    {
        Show(gameOverPanel);
    }

    //------
    private IEnumerator RestartSceneDelayed()
    {
        changingScenes = true;
        yield return sceneFader.FadeOutScene();
        Scenes.RestartScene();
    }

    private IEnumerator ExitGameDelayed()
    {
        changingScenes = true;
        yield return sceneFader.FadeOutScene();
        Scenes.LoadPreviousScene();
    }
}
