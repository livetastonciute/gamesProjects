using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private Transform barrel;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private GameObject muzzlePrefab;

    [SerializeField]
    private UnityEvent onFire;

    private bool hasAmmo;

    public void Awake()
    {
        player = GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {

        if (IsFire())
        {
            Fire();
        }
    }

    public void UpdateAmmo(int value)
    {
        hasAmmo = value > 0;
    }

    private bool IsFire()
    {
        return player.getAmmo() && Input.GetButtonDown("Fire1");
    }

    private void Fire()
    {
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
