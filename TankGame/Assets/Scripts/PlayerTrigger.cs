using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
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

    private bool IsPlayer(Component component)
    {
        return component.gameObject.CompareTag(playerTag);
    }

    private bool IsPlayerExited()
    {
        return colliderCount <= 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            if (IsPlayerExited())
            {
                onPlayerEnter.Invoke();
            }

            colliderCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(IsPlayer(other) && !IsPlayerExited())
        {
            colliderCount--;
        }

        if (IsPlayerExited())
        {
            onPlayerExit.Invoke();
        }
    }
}
