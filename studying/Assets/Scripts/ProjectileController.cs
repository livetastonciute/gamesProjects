using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private Transform barrel;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    GameObject sniperPrefab;
    [SerializeField]
    GameObject rocketPrefab;

    [SerializeField]
    GameObject muzzlePrefab;

    [SerializeField]
    private UnityEvent onFire;


    // Update is called once per frame
    void Update()
    {
        if (IsFire())
        {
            Fire1();
        }

        if (IsFire2())
        {
            Fire2();
        }

        if (IsFire3())
        {
            Fire3();
        }
    }

    private bool IsFire()
    {
        return Input.GetButtonDown("Fire1");
    }

    private bool IsFire2()
    {
        return Input.GetKeyDown(KeyCode.B);
    }

    private bool IsFire3()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    private void Fire1()
    {
        CreateProjectile();
        onFire.Invoke();
    }

    private void Fire2()
    {
        CreateBullet();
        onFire.Invoke();
    }

    private void Fire3()
    {
        CreateRocket();
        onFire.Invoke();
    }

    private void CreateProjectile()
    {
        Instantiate(projectilePrefab, barrel.position, barrel.rotation);
    }

    private void CreateBullet()
    {
        Instantiate(sniperPrefab, barrel.position, barrel.rotation);
    }

    private void CreateRocket()
    {
        Instantiate(rocketPrefab, barrel.position, barrel.rotation);
    }
}
