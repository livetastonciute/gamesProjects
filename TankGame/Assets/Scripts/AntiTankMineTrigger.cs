using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AntiTankMineTrigger : MonoBehaviour
{
    [SerializeField]
    private string antiTankMineTag = "ATMine";

    [SerializeField, Min(0f)]
    private float minExplosionForce = 3f;

    [SerializeField, Min(0f)]
    private float maxExplosionForce = 5f;

    [Header("Effects")]
    [SerializeField]
    private GameObject explosionPrefab;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onTriggerMine;

    private Rigidbody rb;
    
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        var otherGameObject = other.gameObject;
        if (IsAntiTankMine(otherGameObject))
        {
            CreateExplosionEffect(otherGameObject.transform.position);
            AddExplosionForce();
            otherGameObject.SetActive(false);
            Destroy(otherGameObject);
            onTriggerMine.Invoke();
            
        }
    }

    private bool IsAntiTankMine(GameObject obj)
    {
        return obj.CompareTag(antiTankMineTag);
    }

    private void AddExplosionForce()
    {
        var force = Random.Range(minExplosionForce, maxExplosionForce);
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
    private void CreateExplosionEffect(Vector3 position)
    {
        Instantiate(explosionPrefab, position, Quaternion.identity);
    }
}
