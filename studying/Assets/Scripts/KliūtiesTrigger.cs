using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KliūtiesTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent onTrigger;

    [SerializeField]
    UnityEvent onTrigger2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            onTrigger.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onTrigger2.Invoke();
        }
    }
}
