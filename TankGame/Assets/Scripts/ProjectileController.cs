using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private Transform barrel;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private GameObject muzzlePrefab;

    [SerializeField]
    private UnityEvent onFire;

    private bool fireQueued;
    private bool hasAmmo;

    public void UpdateAmmo(int value)
    {
        hasAmmo = value > 0;
    }


    public void Fire()
    {
        if (!hasAmmo)
        {
            return;
        }

        CreateProjectile();
        CreateMuzzleEffect();
        onFire.Invoke();
    }

    private void CreateProjectile()
    {
        Instantiate(projectilePrefab, barrel.position, barrel.rotation);
    }

    private void CreateMuzzleEffect()
    {
        Instantiate(muzzlePrefab, barrel.position, barrel.rotation);
    }
}
