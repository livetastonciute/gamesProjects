using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    [SerializeField]
    Text damageText;

    [SerializeField]
    Text hpText;

    int hp = 20000;

    public void UpdateDamage(int damage)
    {
        damageText.text = "Damage: " + damage;
    }

    public void UpdateHp(int damage)
    {
        hp = hp - damage;
        hpText.text = "HP: " + hp;
    }

    public int getHp() { return hp; }
}
