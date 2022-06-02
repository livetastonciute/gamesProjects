using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnTrigger : MonoBehaviour
{

    [SerializeField]
    private List<BallSpawner> ballSpawner;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var bs in ballSpawner)
            {
                bs.spawn = true;

            }
        }
    }
}
