using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private InputHandler ih;
    private Rigidbody rb;
    private Health health;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    //[Header("Jump Settings")]
    //public float jumpForce = 7f;
    //public float jumpCooldown = 0.2f;
    //public float gravityMultiplier = 2.5f;

    [Header("Dodge Roll Settings")]
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    public float rollCooldown = 1f;
    public float invincibilityDuration = 0.4f;

    private Vector3 inputDirection;
    private Vector3 verticalVelocity;
    private bool isRolling = false;
    private bool canRoll = true;
    private bool canJump = true;

    private void Start()
    {
        ih = FindObjectOfType<InputHandler>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (!isRolling)
        {
            GetInputDirection();
            //HandleJumping();
        }

        if (ih.GetInput(Control.Dodge, KeyPressType.Down) && canRoll && !isRolling) // && controller.isGrounded)
        {
            StartCoroutine(DodgeRoll());
        }
    }

    private void FixedUpdate()
    {
        DoStrafing();
    }

    private void GetInputDirection()
    {
        inputDirection = Vector3.zero;

        // Input handler by Jake
        if (ih.GetInput(Control.Up, KeyPressType.Held))
        {
            inputDirection += Vector3.forward;
        }
        if (ih.GetInput(Control.Down, KeyPressType.Held))
        {
            inputDirection -= Vector3.forward;
        }
        if (ih.GetInput(Control.Left, KeyPressType.Held))
        {
            inputDirection -= Vector3.right;
        }
        if (ih.GetInput(Control.Right, KeyPressType.Held))
        {
            inputDirection += Vector3.right;
        }

        // Look at mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (xzPlane.Raycast(ray, out distance))
        {
            Vector3 pointOfIntersection = ray.GetPoint(distance);
            //Debug.Log("Ray hit the XZ plane at: " + pointOfIntersection);
            pointOfIntersection.y = transform.position.y;
            transform.LookAt(pointOfIntersection);
        }
    }

    private void DoStrafing()
    {
        SetVelocityXZ(inputDirection.normalized * moveSpeed * Time.deltaTime);


        //ApplyGravity();
    }

    private void SetVelocityXZ(Vector3 velocity)
    {
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    //private void HandleJumping()
    //{       
    //    if (ih.GetInput(Control.Jump, KeyPressType.Down) && canJump && !isRolling) // && controller.isGrounded
    //    {
    //        verticalVelocity.y = jumpForce;
    //        canJump = false;
    //        StartCoroutine(ResetJumpCooldown());
    //    }
    //}

    //private void ApplyGravity()
    //{
    //    if (controller.isGrounded && verticalVelocity.y < 0)
    //    {
    //        verticalVelocity.y = -2f;
    //    }
    //    else
    //    {           
    //        verticalVelocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
    //    }
    //    controller.Move(verticalVelocity * Time.deltaTime);
    //}

    //private IEnumerator ResetJumpCooldown()
    //{
    //    yield return new WaitForSeconds(jumpCooldown);
    //    canJump = true;
    //}

    private IEnumerator DodgeRoll()
    {
        isRolling = true;
        health.SetInvincibility(true);

        canRoll = false;

        //Vector3 rollDirection = inputDirection.magnitude > 0.1f ? inputDirection.normalized : transform.forward;
        float startTime = Time.time;
        float endTime = startTime + rollDuration;

        while (Time.time < endTime)
        {
            //controller.Move(rollDirection * rollSpeed * Time.deltaTime);

            //controller.Move(Vector3.down * 2f * Time.deltaTime);

            yield return null;
        }

        isRolling = false;
        health.SetInvincibility(false);

        yield return new WaitForSeconds(rollCooldown);

        canRoll = true;
    }
}