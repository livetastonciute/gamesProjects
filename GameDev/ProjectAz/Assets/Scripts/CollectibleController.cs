using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> collectibles;
    [Min(0)]
    [SerializeField]
    private int count = 3;
    [SerializeField]
    private Vector3 size = new Vector3(16f, 0f, 16f);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    private void Start()
    {
        CreateCollectibles();
    }

    private void CreateCollectibles()
    {
        for (int i = 0; i < count; i++)
        {
            foreach (var collectible in collectibles)
            {
                CreateCollectible(collectible);
            }
        }
    }

    private void CreateCollectible(GameObject collectible)
    {
        Instantiate( 
            collectible,
            GetRandomPosition(),
            collectible.transform.rotation,
            gameObject.transform

        );
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

    public void CreateCollectible()
    {
        var randomCollectible = collectibles.OrderBy(collectible => Random.value).FirstOrDefault();
        if(randomCollectible == null)
        {
            return;
        }
        CreateCollectible(randomCollectible);
    }
}
