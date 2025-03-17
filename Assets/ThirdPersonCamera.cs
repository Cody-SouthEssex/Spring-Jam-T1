using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    public float distance = 10.0f;
    public float height = 5.0f;
    public Vector3 targetOffset = new Vector3(0, 1, 0);

    private float rotationSpeed = 0;
    public bool allowRotationInput = true;
    public float minVerticalAngle = 20.0f;
    public float maxVerticalAngle = 60.0f;

    private float positionSmoothTime = 0f;
    private float rotationSmoothTime = 0f;

    public bool enableCollisionDetection = true;
    public float collisionRadius = 0.2f;
    public LayerMask collisionLayers = -1;
    
    private float currentRotationX = 0f;
    private float currentRotationY = 45f;
    private Vector3 velocity = Vector3.zero;
    private float rotationVelocityX = 0f;
    private float rotationVelocityY = 0f;

    private void Start()
    {
        if (target == null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                target = player.transform;
            }
        }

        Vector3 targetPosition = target.position + targetOffset;
        transform.position = CalculateCameraPosition(targetPosition);
        transform.LookAt(targetPosition);

        Vector3 angles = transform.eulerAngles;
        currentRotationX = angles.y;
        currentRotationY = angles.x;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        if (allowRotationInput)
        {
            float inputX = Input.GetAxis("Mouse X");
            float inputY = Input.GetAxis("Mouse Y");

            if (Input.GetMouseButton(1))
            {
                float targetRotationX = currentRotationX + inputX * rotationSpeed;
                currentRotationX = Mathf.SmoothDampAngle(currentRotationX, targetRotationX, ref rotationVelocityX, rotationSmoothTime);

                float targetRotationY = currentRotationY - inputY * rotationSpeed;
                targetRotationY = Mathf.Clamp(targetRotationY, minVerticalAngle, maxVerticalAngle);
                currentRotationY = Mathf.SmoothDampAngle(currentRotationY, targetRotationY, ref rotationVelocityY, rotationSmoothTime);
            }
        }

        Vector3 targetPosition = target.position + targetOffset;

        Vector3 desiredPosition = CalculateCameraPosition(targetPosition);

        if (enableCollisionDetection)
        {
            desiredPosition = HandleCameraCollision(targetPosition, desiredPosition);
        }

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, positionSmoothTime);

        transform.LookAt(targetPosition);
    }

    private Vector3 CalculateCameraPosition(Vector3 targetPosition)
    {
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);

        Vector3 negDistance = new Vector3(0, 0, -distance);
        Vector3 position = rotation * negDistance + targetPosition;

        position.y += height;

        return position;
    }

    private Vector3 HandleCameraCollision(Vector3 targetPosition, Vector3 desiredPosition)
    {
        RaycastHit hit;
        Vector3 direction = desiredPosition - targetPosition;
        float targetDistance = direction.magnitude;

        if (Physics.SphereCast(targetPosition, collisionRadius, direction.normalized, out hit, targetDistance, collisionLayers))
        {
            return targetPosition + direction.normalized * (hit.distance - collisionRadius);
        }

        return desiredPosition;
    }

    //public void SetCameraAngles(float horizontalAngle, float verticalAngle)
    //{
    //    currentRotationX = horizontalAngle;
   //     currentRotationY = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
    //}

    public void ResetCamera()
    {
        if (target != null)
        {
            currentRotationX = target.eulerAngles.y;
            currentRotationY = 45f;
        }
    }
}