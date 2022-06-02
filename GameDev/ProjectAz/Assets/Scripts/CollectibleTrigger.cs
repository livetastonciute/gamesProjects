using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UI))]
public class CollectibleTrigger : MonoBehaviour
{
    [SerializeField]
    private string coinTag = "Coin";

    [SerializeField]
    private string StartTag = "StartCheckpoint";

    [SerializeField]
    private string keyTag = "Key";

	[SerializeField]
    private string HpTag = "Hp";

    [SerializeField, Min(0)]
    private int coinValue = 1;

	[SerializeField, Min(0)]
    private int hpValue = 20;

    private CollectibleController collectibleController;
    private UI t;
    private Vector3 Checkpoint;
    private bool CpointExists = false;

    //AudioSource animationSoundPlayer; // checkpoint audio 
    public AudioSource[] sounds;
    AudioSource hpSound;
    AudioSource checkpointSound;
    AudioSource starSound;

    //public AudioSource hpAudio; // hp audio

    private void Awake()
    {
        //animationSoundPlayer = GetComponent<AudioSource>();
        sounds = GetComponents<AudioSource>();
        hpSound = sounds[0];
        checkpointSound = sounds[2];
        starSound = sounds[4];

        collectibleController = FindObjectOfType<CollectibleController>();
        t = GetComponent<UI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;
        var collected = false;

        if (IsCoin(otherGameObject)) 
        {
            starSound.Play();
            t.AddCoins(coinValue);
            collected = true;
            if (t.CoinCount() >= 5)
            {
                checkpointSound.Play();
                t.RemoveCoins(5);
                CpointExists = true;
                var temp = otherGameObject; 
                otherGameObject.SetActive(false);
                Destroy(otherGameObject);
                Checkpoint = temp.transform.position;
                Debug.Log(Checkpoint);
            }
        } 
        if(isKey(otherGameObject))
        {
            t.AddKeys(1);
            collected = true;
        }
	    if (isHp(otherGameObject))
        {
            //animationSoundPlayer.Play(); // hp sound
            hpSound.Play();

            t.AddHp(hpValue);
            collected = true;
        }
        if (isStartCheckpoint(otherGameObject))
        {
            CpointExists = true;
            var temp = otherGameObject;
            otherGameObject.SetActive(false);
            Destroy(otherGameObject);
            Checkpoint = temp.transform.position;
            Debug.Log(Checkpoint);
        }
        if (collected)
        {
            otherGameObject.SetActive(false);
            Destroy(otherGameObject);
        }
    }

    private bool isKey(GameObject obj)
    {
        return obj.CompareTag(keyTag);
    }

    private bool IsCoin(GameObject obj)
    {
        return obj.CompareTag(coinTag);
    }

	private bool isHp(GameObject obj)
    {
        return obj.CompareTag(HpTag);
    }

    private bool isStartCheckpoint(GameObject obj)
    {
        return obj.CompareTag(StartTag);
    }

    public bool CheckpointExists()
    {
        return CpointExists;
    }
    public Vector3 GetCheckpoint()
    {
        return Checkpoint;
    }

}
