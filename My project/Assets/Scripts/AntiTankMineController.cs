using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTankMineController : MonoBehaviour
{
    [SerializeField]
    private int count = 3;

    [SerializeField]
    private Vector3 size = new Vector3(16f, 0, 16f);

    [SerializeField]
    private GameObject mine;
    // Start is called before the first frame update
    void Start()
    {
        CreateMines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }
    public void CreateMines()
    {
        for(int i = 0; i <= count; i++)
        {
            CreateMine();
        }
    }

    private void CreateMine()
    {
        Instantiate(mine, getRandomPosition(), mine.transform.rotation, gameObject.transform);
    }

    private Vector3 getRandomPosition()
    {
        var position = new Vector3(
            Random.Range(0, size.x),
            Random.Range(0, size.y),
            Random.Range(0, size.z));

        return transform.position + position - size / 2;
    }
}
