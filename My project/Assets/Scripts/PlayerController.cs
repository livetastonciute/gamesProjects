using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.5f;

    [SerializeField]
    private float rotationSpeed = 60f;

    [SerializeField]
    private float jumpForce = 10f;

    private Rigidbody rb;
    private Vector2 movementAxis;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateMovementAxis();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdateMovementAxis()
    {
        movementAxis.x = Input.GetAxis("Horizontal");
        movementAxis.y = Input.GetAxis("Vertical");
    }

    private void UpdatePosition()
    {
        var positionMovement = transform.forward * (
            movementAxis.y * moveSpeed * Time.deltaTime);

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
