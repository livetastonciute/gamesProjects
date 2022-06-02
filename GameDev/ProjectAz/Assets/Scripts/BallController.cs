using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 10f;
    [SerializeField]
    private string destroyerTag = "Destroyer";
    [SerializeField]
    private string playertag = "Player";
    [SerializeField]
    private int force = 200;
    CharacterController character;

    private void Awake()
    {
        character = FindObjectOfType<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destruction();
            }
        }
        if (this.transform.position.y <= -10)
        {
            Destruction();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == destroyerTag)
        {
            Destruction();
        }
        if (obj.tag == playertag)
        {
            var dir = obj.transform.position - this.gameObject.transform.position;
            ImpactReceiver.AddImpact(dir, force);
        }
    }
    private void Destruction()
    {
        Destroy(this.gameObject);
    }
}