using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusicController : MonoBehaviour
{
    [Header("Lives")]
    [Min(0)]
    [SerializeField]
    private int livesThreshold = 3;

    [Min(0)]
    [SerializeField]
    private int livesIncreaseAmount = 1;

    [Min(0)]
    [SerializeField]
    private int livesDecreaseAmount = 2;

    [Header("Ammo")]
    [Min(0)]
    [SerializeField]
    private int ammoThreshold;

    [Min(0)]
    [SerializeField]
    private int ammoDecreaseAmount = 5;

    [Header("General")]
    [SerializeField]
    private float playDelay = 1f;

    private MusicController musicController;
    private float startPlayingAt;
    private int currentLives;

    private void Awake()
    {
        musicController = GetComponent<MusicController>();
        startPlayingAt = Time.time + playDelay;
    }

    public void UpdateLives(int lives)
    {
        if (IsPlaying())
        {
            if(currentLives < lives && livesThreshold < lives)
            {
                PlayRandom(livesIncreaseAmount);
            }
            else if (currentLives > lives && livesThreshold >= lives)
            {
                StopRandom(livesDecreaseAmount);
            }
        }
        currentLives = lives;
    }
    public void UpdateAmmo(int ammo)
    {
        if (IsPlaying())
        {
            if (ammoThreshold >= ammo)
            {
                StopRandom(ammoDecreaseAmount);
            }
        }
    }
    private bool IsPlaying()
    {
        return Time.time >= startPlayingAt;
    }
    private void PlayRandom(int times)
    {
        for(var index = 0; index < times; index++)
        {
            musicController.PlayRandom();
        }
    }
    private void StopRandom(int times)
    {
        for (var index = 0; index < times; index++)
        {
            musicController.StopRandom();
        }
    }
}
