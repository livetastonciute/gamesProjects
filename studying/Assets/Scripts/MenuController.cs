using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    RectTransform pausePanel;

    [SerializeField]
    Text hpText;

    int hp = 0;

    public void ExitGame()
    {
        Scenes.ExitGame();
    }

    private static void Hide(Component component)
    {
        component.gameObject.SetActive(false);
    }

    private static void Show(Component component)
    {
        component.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Scenes.RestartScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !(pausePanel.gameObject.active))
        {
            Show(pausePanel);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.gameObject.active)
        {
            Hide(pausePanel);
        }

        hpText.text = "HP: " + hp;

    }

    private void Start()
    {
        Hide(pausePanel);
    }

    public void AddHp()
    {
        hp += 20;
    }
}
