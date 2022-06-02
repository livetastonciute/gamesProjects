using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    [SerializeField]
    private GameObject explosionPrefab;

    [Min(0f)]
    [SerializeField]
    private float velocity = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * velocity;
    }

    private void OnCollisionEnter()
    {
        Destroy(gameObject);
        CreateExplosionEffect();
    }

    private void CreateExplosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
