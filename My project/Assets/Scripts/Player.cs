using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private int ammo = 5;

    [Header("UI")]
    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text ammoText;

    [SerializeField]
    private Text gameOverText;

    private PlayerController controller;
    private GamePlayController gamePlayController;

    [Header("Events")]
    [SerializeField]
    private IntUnityEvent onUpdateLives;

    [SerializeField]
    private IntUnityEvent onUpdateAmmo;

    [SerializeField]
    private UnityEvent onStopGame;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        gamePlayController = FindObjectOfType<GamePlayController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateLivesText();
        UpdateAmmoText();
    }

    private void UpdateLivesText()
    {
        livesText.text = $"Lives: {lives}";
    }

    private void UpdateAmmoText()
    {
        ammoText.text = $"Ammo: {ammo}";
    }

    public void TakeDamage()
    {
        lives--;
        UpdateLivesText();

        if(lives <= 0)
        {
            StopGame();
        }
    }

    public void StopGame()
    {
        controller.enabled = false;
        onStopGame.Invoke();
    }

    public void AddLives(int value)
    {
        lives += value;
        UpdateLivesText();
    }

    public void AddAmmo(int value)
    {
        ammo += value;
        UpdateAmmoText();
    }

    public bool getAmmo()
    {
        return ammo > 0;
    }
}
