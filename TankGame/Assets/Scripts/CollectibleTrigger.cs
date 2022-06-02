using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Player))]
public class CollectibleTrigger : MonoBehaviour
{
    [SerializeField]
    private string medicalPillTag = "MedicalPill";

    [SerializeField]
    private string ammoTag = "Ammo";

    [SerializeField, Min(0)]
    private int pillValue = 1;

    [SerializeField, Min(0)]
    private int ammoValue = 2;

    [Header("Events")]
    [SerializeField]
    private IntUnityEvent onCollectLives;

    [SerializeField]
    private IntUnityEvent onCollectAmmo;

    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;
        var collected = false;

        if(IsMedicalPill(otherGameObject))
        {
            onCollectLives.Invoke(pillValue);
            collected = true;
        }
        else if (IsAmmo(otherGameObject))
        {
            onCollectAmmo.Invoke(ammoValue);
            collected = true;
        }
        if (collected)
        {
            otherGameObject.SetActive(false);
            Destroy(otherGameObject);
        }
    }

    private bool IsMedicalPill(GameObject obj)
    {
        return obj.CompareTag(medicalPillTag);
    }

    private bool IsAmmo(GameObject obj)
    {
        return obj.CompareTag(ammoTag);
    }
}
