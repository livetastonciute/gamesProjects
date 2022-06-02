using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]
    private float moveSpeed = 2.5f;

    [Min(0f)]
    [SerializeField]
    private float rotationSpeed = 60f;

    private Rigidbody rb;
    private Vector2 movementAxis;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        UpdatePosition();
        UpdateRotation();
    }

    public void Move(Vector2 newMovementAxis)
    {
        movementAxis = newMovementAxis;
    }

    private void UpdatePosition()
    {
        var positionMovement = transform.forward *
            (movementAxis.y * moveSpeed * Time.deltaTime);

        var currentPosition = rb.position;
        var newPosition = currentPosition + positionMovement;

        rb.MovePosition(newPosition);
    }

    private void UpdateRotation()
    {
        var rotationMovement = movementAxis.x * rotationSpeed * Time.deltaTime;
        var currentRotation = rb.rotation.eulerAngles;
        currentRotation.y += rotationMovement;

        var newRotation = Quaternion.Euler(currentRotation);

        rb.MoveRotation(newRotation);
    }
}
