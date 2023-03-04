using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePos : MonoBehaviour
{ 
    public float mouseSpeed;
    public Transform cameraHolder;

    public float minY;
    public float maxY;

    private float pitch;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    void Update()
    {
        UpdatePitch();
        UpdateYaw();
    }
    private void UpdateYaw()
    {
        float hInput = Input.GetAxis("Mouse X");
        transform.Rotate(0, hInput * mouseSpeed, 0);
    }
    private void UpdatePitch()
    {
        float vInput = Input.GetAxis("Mouse Y");
        pitch -= vInput * mouseSpeed;
        pitch = Mathf.Clamp(pitch, minY, maxY);
        cameraHolder.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

}
