using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private UnityEvent onPlayerEnter;

    [SerializeField]
    private UnityEvent onPlayerExit;

    private int colliderCount;

    private void Awake()
    {
        var triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            if (IsPlayerExited())
            {
                onPlayerEnter.Invoke();
                Debug.Log("enter");
            }
           colliderCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag) && !IsPlayerExited())
        {
            colliderCount--;
        }
        if (IsPlayerExited())
        {
            onPlayerExit.Invoke();
            Debug.Log("exit");
        }
    }

    private bool IsPlayerExited()
   {
        return colliderCount <= 0;
    }
}
