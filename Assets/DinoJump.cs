using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))] 
public class DinoJump : MonoBehaviour
{
    public float jumpForce = 8f; // Boosted slightly, 5f can feel heavy in 3D physics

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;

    // Renamed variable from 'rigidbody' to 'rb' to prevent Unity keyword conflicts
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Safety check to ensure you assigned the object in the inspector
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame &&
            isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
