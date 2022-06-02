using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent onCollect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hp"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            onCollect.Invoke();
        }
    }
}
