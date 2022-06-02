using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichTile : MonoBehaviour
{

    UI scriptUI;

    private void Awake()
    {
        scriptUI = FindObjectOfType<UI>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Red"))
        {
            Debug.Log("Mirtis");
            gameObject.SetActive(false);
            Destroy(gameObject);
            scriptUI.SetActiveCanvas();
        }

        if (collision.gameObject.CompareTag("White"))
        {
            Debug.Log("Saugu");
        }
    }
}
