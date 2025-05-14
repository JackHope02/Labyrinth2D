using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 120f;

    private CharacterController controller;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Forward/Back movement (Z axis)
        float moveInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveInput * currentSpeed;

        // Y-axis rotation (A/D)
        float rotateInput = Input.GetAxis("Horizontal");
        transform.Rotate(0f, rotateInput * rotationSpeed * Time.deltaTime, 0f);

        controller.SimpleMove(move);
    }
}
