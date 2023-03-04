using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float speed;

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * vInput + transform.right * hInput;

        controller.SimpleMove(direction * speed);
    }
}
