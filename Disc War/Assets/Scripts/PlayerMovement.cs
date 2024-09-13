using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents body rotation when moving
    }

    private void Update() {
        MyInput();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    // Update is called once per frame
    private void MyInput() {
        // Receives user input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer() {
        // Calculates movement direction
        moveDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
