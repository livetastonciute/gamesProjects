using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    GameObject explosionPrefab;

    [SerializeField]
    float velocity = 10f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = transform.forward * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        CreateExplosionEffect();
        Debug.Log("Sprogimas");
    }

    private void CreateExplosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
