using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//veikejo .cs valdytojo pavadinimas
[RequireComponent(typeof(CharacterController))]
public class UI : MonoBehaviour
{
    [Header("Player state")]
    [SerializeField, Min(0)]
    private int coins = 0;

    [SerializeField, Min(0)]
    public int keys = 0;

	[SerializeField, Min(0)]
    public int hp = 100;

    public int lives = 3;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private TextMeshProUGUI keystext;

	[SerializeField]
    private TextMeshProUGUI hpText;

    [SerializeField]
    public Image heart1, heart2, heart3;

    private CharacterController t;

    private void Awake()
    {
        t = GetComponent<CharacterController>();
    }
    void Start()
    {
        UpdateCoinsText();
        UpdateKeysText();
		UpdateHpText();
    }

	private void UpdateHpText()
	{
		hpText.text = $"HP: {hp}";
	}

	public void AddHp(int value)
    {
        if(hp < 100)
	    {
		    hp += value;
            if(hp > 100)
            {
                hp = 100;
            }
            UpdateHpText();
	    }    
    }

	public void RemoveHp(int value)
    {
        hp -= value;
        UpdateHpText();
    }

    private void UpdateKeysText()
    {
        keystext.text = $"Keys: {keys}";
    }

    private void UpdateCoinsText()
    {
        coinsText.text = $"STARS: {coins}";
    }
    public void AddCoins(int value)
    {
        coins += value;
        UpdateCoinsText();
    }
    public void AddKeys(int value)
    {
        keys += value;
        UpdateKeysText();
    }

    public void RemoveKeys(int value)
    {
        keys -= value;
        UpdateKeysText();
    }
    public void RemoveCoins(int value)
    {
        coins -= value;
        UpdateCoinsText();
    }
    // Update is called once per frame

    public bool HasKeys()
    {
        return keys > 0;
    }

    public int CoinCount()
    {
        return coins;
    }

    public void RemoveLife()
    {
        lives -= 1;
        if(lives == 2)
        {
            heart3.enabled = false;
        }
        if (lives == 1)
        {
            heart2.enabled = false;
        }
        if (lives == 0)
        {
            heart1.enabled = false;
        }
    }
    void Update()
    {
        
    }
}
