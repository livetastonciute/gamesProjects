using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayCanvasController : MonoBehaviour
{

    [Header("Collectibles")]
    [SerializeField]
    private RectTransform heartContainer;

    [SerializeField]
    private GameObject heartPrefab;

    [Header("Menus")]
    [SerializeField]
    private RectTransform pausedMenu;
    [SerializeField]
    private RectTransform nextLevelMenu;
    [SerializeField]
    private RectTransform gameOverMenu;

    private SceneFader sceneFader;
    private bool changingScenes;

    //public AudioSource sound;

    private void Awake()
    {
        sceneFader = GetComponent<SceneFader>();
        //sound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Hide(pausedMenu);
        Hide(nextLevelMenu);
        Hide(gameOverMenu);
        //sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            TogglePausedGame();
        }
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        Hide(pausedMenu);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        if (changingScenes)
        {
            return;
        }
        StartCoroutine(ExitGameDelayed());
    }

    private IEnumerator ExitGameDelayed()
    {
        changingScenes = true;
        yield return sceneFader.FadeOutScene();
        //sound.Stop();
        // play menu song
        SceneManager.LoadScene("StartMenu");

    }

    private void TogglePausedGame()
    {
        if(IsGamePaused())
        {
            ResumeGame();
            //sound.UnPause();
        }
        else
        {
            PauseGame();
            //sound.Pause();
        }
    }

    private void PauseGame()
    {
        Show(pausedMenu);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        if (changingScenes)
        {
            return;
        }
        StartCoroutine(NextLevelDelayed());

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator NextLevelDelayed()
    {
        changingScenes = true;
        yield return sceneFader.FadeOutScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private bool IsGamePaused()
    {
        return pausedMenu.gameObject.activeInHierarchy;
    }

    private static void Show(Component component)
    {
        component.gameObject.SetActive(true);
    }

    private static void Hide(Component component)
    {
        component.gameObject.SetActive(false);
    }
}
