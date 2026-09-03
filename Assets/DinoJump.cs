using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class DinoJump : MonoBehaviour
{
    public float jumpForce = 8f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool hasJumped;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Prevent the dinosaur from tipping over
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Check if we're touching the ground
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(
                groundCheck.position,
                groundDistance,
                groundMask
            );
        }

        // Reset jump when we are back on the ground
        if (isGrounded)
        {
            hasJumped = false;
        }

        // Jump only if we haven't already jumped
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame &&
            isGrounded &&
            !hasJumped)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasJumped = true;
        }
    }
}