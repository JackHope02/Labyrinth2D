using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;
    private float currentY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock all unwanted physics-based motion
        rb.constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;

        // Cache initial Y rotation
        currentY = transform.eulerAngles.y;
    }

    void Update()
    {
        // Handle rotation input (left/right keys)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            currentY -= rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            currentY += rotationSpeed * Time.deltaTime;
        }

        // Apply rotation only on Y-axis
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentY, transform.eulerAngles.z);

        // Move forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        // --- Add running logic here if needed ---
    }
}
