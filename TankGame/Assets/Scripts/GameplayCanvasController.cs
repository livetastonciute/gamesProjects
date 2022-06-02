using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayCanvasController : MonoBehaviour
{
    [Header("Collectibles")]
    [SerializeField]
    private RectTransform heartContainer;

    [SerializeField]
    private RectTransform ammoContainer;

    [SerializeField]
    private GameObject heartPrefab;

    [SerializeField]
    private GameObject ammoPrefab;

    [Header("Menus")]
    [SerializeField]
    private RectTransform pausedMenu;

    [SerializeField]
    private RectTransform gameOverMenu;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onShowGameOver;

    [SerializeField]
    private UnityEvent onResumeGame;

    [SerializeField]
    private UnityEvent onPauseGame;

    private SceneFader sceneFader;
    private bool changingScenes;

    private void Awake()
    {
        sceneFader = GetComponent<SceneFader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide(pausedMenu);
        Hide(gameOverMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            TooglePauseGame();
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void UpdateHeartsUI(int newLives)
    {
        UpdateChildCount(heartContainer, heartPrefab, newLives);
    }

    public void UpdateAmmoUI(int newAmmo)
    {
        UpdateChildCount(ammoContainer, ammoPrefab, newAmmo);
    }
    private void TooglePauseGame()
    {
        if (IsGameOver())
        {
            return;
        }
        if (IsGamePaused())
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void ShowGameOverMenu()
    {
        Hide(pausedMenu);
        Show(gameOverMenu);
        onShowGameOver.Invoke();
    }

    public void ResumeGame()
    {
        Hide(pausedMenu);
        Time.timeScale = 1;
        onResumeGame.Invoke();
    }

    public void RestartGame()
    {
        if (changingScenes)
        {
            return;
        }

        StartCoroutine(RestartGameDelayed());
    }

    public void ExitGame()
    {
        if (changingScenes)
        {
            return;
        }

        StartCoroutine(ExitGameDelayed());
    }

    private IEnumerator RestartGameDelayed()
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

    private void PauseGame()
    {
        Show(pausedMenu);
        Time.timeScale = 0;
        onPauseGame.Invoke();
    }

    private bool IsGameOver()
    {
        return gameOverMenu.gameObject.activeInHierarchy;
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

    private static void UpdateChildCount(Transform container, GameObject prefab, int newCount)
    {
        var childCount = container.childCount;
        var change = Mathf.Abs(childCount - newCount);

        if(childCount < newCount)
        {
            AddChildren(container, prefab, change);
        }
        else
        {
            RemoveChildren(container, change);
        }
    }

    private static void AddChildren(Transform container, GameObject prefab, int count)
    {
        for(var i = 0; i < count; i++)
        {
            Instantiate(prefab, container);
        }
    }

    private static void RemoveChildren(Transform container, int count)
    {
        for (var i = count - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }
}
