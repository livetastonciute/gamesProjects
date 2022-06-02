using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCheck : MonoBehaviour
{
    Vector3 coordinates = new Vector3(0, 0, 0);

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject player2;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
       if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SaveCoordinates();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadCoordinates();
        }
    }

    public void SaveCoordinates()
    {
        coordinates = player.transform.position;
        Debug.Log("Saugota" + coordinates);
    }

    public void LoadCoordinates()
    {
        player.SetActive(false);
        Destroy(player);
        Instantiate(player2, coordinates, transform.rotation, transform);
    }
}
