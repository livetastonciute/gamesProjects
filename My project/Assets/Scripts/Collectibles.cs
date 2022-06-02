using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntUnityEvent : UnityEvent<int>
{
}

public class Collectibles : MonoBehaviour
{
    [SerializeField]
    private string tag1 = "Capsule";

    [SerializeField]
    private string tag2 = "Ammo";

    [SerializeField]
    private int ammoValue = 2;

    [SerializeField]
    private int pillValue = 3;

    private CollectibleController controller;

    private Player player;

    [Header ("Events")]
    [SerializeField]
    private IntUnityEvent onCollectLives;

    [SerializeField]
    private IntUnityEvent onCollectAmmo;


    private void Awake()
    {
        controller = FindObjectOfType<CollectibleController>();
        player = GetComponent<Player>();
    }

    public bool IsItCapsule(GameObject obj)
    {
        return obj.CompareTag(tag1);
    }

    public bool IsItAmmo(GameObject obj)
    {
        return obj.CompareTag(tag2);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (IsItAmmo(other.gameObject))
        {
            onCollectAmmo.Invoke(ammoValue);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }

        else if (IsItCapsule(other.gameObject))
        {
            onCollectLives.Invoke(pillValue);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
