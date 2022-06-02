using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggerProjectile;

    private void OnCollisionEnter(Collision other)
    {
       // Debug.Log("collision įvyko");
        var projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile != null)
        {
            onTriggerProjectile.Invoke();
        }
    }
}
