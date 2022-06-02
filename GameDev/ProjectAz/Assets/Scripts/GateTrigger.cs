using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UI))]
public class GateTrigger : MonoBehaviour
{
    [SerializeField]
    private string gateTag = "Gate";


    [SerializeField, Min(0)]
    private int lockValue = 1;

    private CollectibleController collectibleController;
    private UI t;
    private GateController gateController;


    private void Awake()
    {
        collectibleController = FindObjectOfType<CollectibleController>();
        t = GetComponent<UI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;
        if (IsGate(otherGameObject))
        {
            if(t.keys >= lockValue)
            {
                t.RemoveKeys(lockValue);
                Destroy(otherGameObject);
            }
        }
    }

    private bool IsGate(GameObject obj)
    {
        return obj.CompareTag(gateTag);
    }

}

