using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;


    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    public bool spawn = false;


    [SerializeField]
    private Vector3 size = new Vector3(16f, 0f, 16f);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    private void Start()
    {
        InvokeRepeating("CreateBall", spawnTime, spawnDelay);
    }

    private void CreateBall()
    {
        if (spawn)
        {
            Instantiate(
                ball,
                GetRandomPosition(),
                ball.transform.rotation
                //gameObject.transform
                );
        }
        if (!spawn)
        {
            //CancelInvoke("CreateBall");
        }
    }

    private Vector3 GetRandomPosition()
    {
        var volumePosition = new Vector3(
            Random.Range(0, size.x),
            Random.Range(0, size.y),
            Random.Range(0, size.z)
            );
        return transform.position + volumePosition - size / 2;
    }
}
