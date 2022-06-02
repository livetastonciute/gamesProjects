using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AntiTankMineTrigger : MonoBehaviour
{
    private AntiTankMineController tankMineController;

    private Rigidbody rb;

    private Player player;

    [SerializeField]
    private float minForce = 1f;

    [SerializeField]
    private float maxForce = 3f;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onTriggerMine;

    [SerializeField]
    private ParticleSystem explosion;

    private void Awake()
    {
        tankMineController = FindObjectOfType<AntiTankMineController>();
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Mine"))
        {
            AddForce();
            player.TakeDamage();
            CreateParticleObject(other.gameObject);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            onTriggerMine.Invoke();
            

        }
    }
    private void AddForce()
    {
        var force = Random.Range(minForce, maxForce);

        rb.AddForce(Vector3.up * force, ForceMode.Impulse);        
    }

    private void CreateParticleObject(GameObject gb)
    {
        Instantiate(explosion, gb.transform.position, explosion.transform.rotation, transform);
    }
}
