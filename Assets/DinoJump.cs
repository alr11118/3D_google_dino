using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class DinoJump : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = 20f;

    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;

    [Header("Ducking")]
    private bool isDucking = false;
    private float originalHeight;
    private Vector3 originalCenter;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Save the original collider dimensions
        originalHeight = controller.height;
        originalCenter = controller.center;
    }

    void Update()
    {
        // FORWARD MOVEMENT
        moveDirection.z = forwardSpeed;


        // JUMP + GRAVITY
        if (controller.isGrounded)
        {
            // Keep the dinosaur on the ground
            moveDirection.y = -1f;

            // Jump
            if (Keyboard.current.upArrowKey.wasPressedThisFrame ||
                Keyboard.current.wKey.wasPressedThisFrame ||
                Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                moveDirection.y = jumpForce;

                // Stop ducking when jumping
                if (isDucking)
                {
                    StopDuck();
                }
            }
        }
        else
        {
            // Gravity
            moveDirection.y -= gravity * Time.deltaTime;
        }


        // Duck
        if (Keyboard.current.downArrowKey.wasPressedThisFrame ||
            Keyboard.current.sKey.wasPressedThisFrame)
        {
            StartDuck();
        }

        if (Keyboard.current.downArrowKey.wasReleasedThisFrame ||
            Keyboard.current.sKey.wasReleasedThisFrame)
        {
            StopDuck();
        }


        // APPLY MOVEMENT
        controller.Move(moveDirection * Time.deltaTime);
    }


    void StartDuck()
    {
        if (isDucking)
            return;

        isDucking = true;

        // Make the collider half as tall
        controller.height = originalHeight / 2f;

        // Move the center DOWN so the bottom of the collider
        // stays approximately in the same place.
        controller.center = new Vector3(
            originalCenter.x,
            originalCenter.y - originalHeight / 4f,
            originalCenter.z
        );
    }


    void StopDuck()
    {
        if (!isDucking)
            return;

        isDucking = false;

        // Restore collider
        controller.height = originalHeight;
        controller.center = originalCenter;
    }
}