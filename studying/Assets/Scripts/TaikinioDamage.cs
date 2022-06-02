using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaikinioDamage : MonoBehaviour
{
    DamageUI damageUI;

    private void Awake()
    {
        damageUI = FindObjectOfType<DamageUI>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sniper"))
        {
            Debug.Log("pašautas su sniper");
            damageUI.UpdateDamage(500);
            damageUI.UpdateHp(500);
        }

        if (collision.gameObject.CompareTag("Rocket"))
        {
            Debug.Log("pašautas su raketa");
            damageUI.UpdateDamage(10000);
            damageUI.UpdateHp(10000);
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("pašautas su kulka");
            damageUI.UpdateDamage(100);
            damageUI.UpdateHp(100);
        }
    }

    public void Update()
    {
        if (damageUI.getHp() <= 0)
            Destroy(gameObject);
    }
}
