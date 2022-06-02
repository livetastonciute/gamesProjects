using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    int count = 2;

    [SerializeField]
    List<GameObject> items;

    [SerializeField]
    Vector3 size = new Vector3(16f, 0, 16f);
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateCollectibles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCollectibles()
    {
        for(int i = 0; i < count; i++)
        {
            foreach(GameObject item in items){
                Instantiate(item, GetRandomPosition(), transform.rotation, transform);
            }
        }

        //Random generavimas
     //   for(int i = 0; i < count; i++)
     //   {
     //       int rdmPosition = Random.Range(0, items.Count);
     //       Instantiate(items[rdmPosition], GetRandomPosition(), transform.rotation, transform);
    //    }
    }

    public Vector3 GetRandomPosition()
    {
        var position = new Vector3(
            Random.Range(0, size.x),
            Random.Range(0, size.y),
            Random.Range(0, size.z));
        return position;
    }
}
