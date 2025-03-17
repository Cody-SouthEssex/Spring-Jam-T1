using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public float jumpCooldown = 0.2f;
    public float gravityMultiplier = 2.5f;

    [Header("Dodge Roll Settings")]
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    public float rollCooldown = 1f;
    public float invincibilityDuration = 0.4f;

    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 moveDirection;
    private Vector3 verticalVelocity;
    private bool isRolling = false;
    private bool canRoll = true;
    private bool canJump = true;
    private bool isInvincible = false;

    public System.Action<bool> OnInvincibilityChanged;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (!isRolling)
        {
            HandleMovement();
            HandleJumping();
        }

        if (Input.GetKeyDown(KeyCode.Q) && canRoll && !isRolling && controller.isGrounded)
        {
            StartCoroutine(DodgeRoll());
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * vertical + right * horizontal;

        if (moveDirection.magnitude > 0.1f)
        {
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        ApplyGravity();
    }

    private void HandleJumping()
    {       
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && canJump && !isRolling)
        {
            verticalVelocity.y = jumpForce;
            canJump = false;
            StartCoroutine(ResetJumpCooldown());
        }
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }
        else
        {           
            verticalVelocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    private IEnumerator ResetJumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private IEnumerator DodgeRoll()
    {
        isRolling = true;
        canRoll = false;

        StartCoroutine(InvincibilityFrames());

        Vector3 rollDirection = moveDirection.magnitude > 0.1f ? moveDirection.normalized : transform.forward;
        float startTime = Time.time;
        float endTime = startTime + rollDuration;

        while (Time.time < endTime)
        {
            controller.Move(rollDirection * rollSpeed * Time.deltaTime);

            controller.Move(Vector3.down * 2f * Time.deltaTime);

            yield return null;
        }

        isRolling = false;
       
        yield return new WaitForSeconds(rollCooldown);

        canRoll = true;
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        OnInvincibilityChanged?.Invoke(true);

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
        OnInvincibilityChanged?.Invoke(false);
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}