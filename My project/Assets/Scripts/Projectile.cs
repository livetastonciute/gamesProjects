using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private float velocity = 2f;

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

    // Update is called once per frame
    private void OnCollisionEnter()
    {
        Destroy(gameObject);
        CreateCollisionEffect();
    }

    private void CreateCollisionEffect()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
