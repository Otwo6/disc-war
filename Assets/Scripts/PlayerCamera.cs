using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerCam : NetworkBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if(!IsOwner) return;
        
        // Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation -= mouseY;

        // Prevents camera going beyond feet-to-apex of sky view
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Camera Rotation and Orientation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        orientation.Rotate(Vector3.up * mouseX);
        
    }
}
