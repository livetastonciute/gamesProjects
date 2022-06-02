using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerStart : MonoBehaviour
{
    [SerializeField]
    Text timeText;
    private void OnTriggerEnter(Collider other)
    {
        timeText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        timeText.gameObject.SetActive(false);
    }
}
