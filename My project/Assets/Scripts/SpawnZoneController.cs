using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnZoneController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> prefabs;

    [SerializeField]
    int count = 10;

    private List<SpawnZone> spawnZones;

    private void Awake()
    {
        spawnZones = FindObjectsOfType<SpawnZone>().ToList();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateAll();
    }

    public void Create()
    {
        if(prefabs.Count == 0)
        {
            return;
        }

        var randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
        Create(randomPrefab);
    }

    private void Create(GameObject obj)
    {
        if(spawnZones.Count == 0)
        {
            return;
        }

        var randomSpawnZone = spawnZones[Random.Range(0, spawnZones.Count)];
        randomSpawnZone.Create(obj);
    }

    private void CreateAll()
    {
        foreach(var prefab in prefabs)
        {
            for(var i = 0; i <= count; i++)
            {
                Create(prefab);
            }
        }
    }


}
