using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMaker : MonoBehaviour
{
    [SerializeField]
    int width;

    [SerializeField]
    int height;

    [SerializeField]
    GameObject tile1;

    [SerializeField]
    GameObject tile2;

    [SerializeField]
    GameObject player;

    int x = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (x == 0)
                {
                    Instantiate(tile2, new Vector3(i, 0, j), transform.rotation);
                    x = 1;
                }

                else
                {
                    Instantiate(tile1, new Vector3(i, 0, j), transform.rotation);
                    x = 0;
                }
                    
            }
        }

        Instantiate(player, new Vector3(0, 2, 0), transform.rotation);
    }
}
