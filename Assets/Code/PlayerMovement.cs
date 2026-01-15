using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float dashForce = 15f;       // How strong the dash is
    public float dashDuration = 0.2f;   // How long the dash lasts
    public float dashCooldown = 1f;     // Time before you can dash again
    public LayerMask groundLayer;
    public Transform cameraTransform;

    Rigidbody rb;
    Vector2 moveInput;
    bool isGrounded;

    bool isDashing = false;
    float dashTimer = 0f;
    float dashCooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // INPUT
        moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
        if (Keyboard.current.dKey.isPressed) moveInput.x += 1;

        // GROUND CHECK
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        // JUMP
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                jumpForce,
                rb.linearVelocity.z
            );
        }

        // DASH INPUT
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && dashCooldownTimer <= 0f && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        // Cooldown countdown
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        // CAMERA RELATIVE DIRECTIONS
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;

        if (isDashing)
        {
            rb.linearVelocity = new Vector3(
                move.x * dashForce,
                rb.linearVelocity.y,
                move.z * dashForce
            );

            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0f)
                isDashing = false;
        }
        else
        {
            rb.linearVelocity = new Vector3(
                move.x * moveSpeed,
                rb.linearVelocity.y,
                move.z * moveSpeed
            );
        }
    }
}