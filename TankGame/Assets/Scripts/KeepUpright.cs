using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class KeepUpright : MonoBehaviour
{
    [SerializeField]
    private float xAngle = 20f;

    [SerializeField]
    private float zAngle = 20f;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ClampRigidBodyRotation();
    }
    private void ClampRigidBodyRotation()
    {
        var angles = rigidbody.rotation.eulerAngles;
        angles.x = ClampAngle(angles.x, -xAngle, xAngle);
        angles.z = ClampAngle(angles.z, -zAngle, zAngle);

        var rotation = Quaternion.Euler(angles);
        rigidbody.MoveRotation(rotation);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if(angle < 0f)
        {
            angle = 360 + angle;
        }
        if(angle > 180f)
        {
            return Mathf.Max(angle, 360 + min);
        }
        return Mathf.Min(angle, max);
    }
}
