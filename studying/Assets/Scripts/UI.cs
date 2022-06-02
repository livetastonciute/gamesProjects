using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    RectTransform canvas;

    public void RestartGame()
    {
        
        Instantiate(player, new Vector3(0, 2, 0), transform.rotation);
        canvas.gameObject.SetActive(false);
    }

    public void SetActiveCanvas()
    {
        canvas.gameObject.SetActive(true);
    }
}
