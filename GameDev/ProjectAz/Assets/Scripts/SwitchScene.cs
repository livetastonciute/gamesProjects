using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField]
    public GameObject FinishPanel;

    [SerializeField]
    public ParticleSystem confetti1;

    [SerializeField]
    public ParticleSystem confetti2;

    AudioSource animationSoundPlayer;

    private void Start()
    {
        FinishPanel.SetActive(false);
        animationSoundPlayer = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StageEnd")
        {
            animationSoundPlayer.Play();

            confetti1.Play();
            confetti2.Play();
            FinishPanel.SetActive(true);
            ThridPersonMovement.playerControlsEnabled = false;
            //Time.timeScale = 0;

        }
    }

}
