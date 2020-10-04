using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxMovementSpeed = 5f;

    public float MaxMovementSpeed
    {
        get
        {
            return maxMovementSpeed;
        }

        private set
        {
            maxMovementSpeed = value;
        }
    }

    private Vector3 movementVelocity;
    public Vector3 MovementVelocity
    {
        get
        {
            return movementVelocity;
        }

        private set
        {
            movementVelocity = value;
        }
    }

    private Vector3 lookDirection;

    public Vector3 LookDirection
    {
        get
        {
            return lookDirection;
        }

        private set
        {
            lookDirection = value;
        }
    }

    private PlayerInput playerInput;

    private CharacterController characterController;

    private Camera mainCamera;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        characterController = GetComponent<CharacterController>();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        Vector3 movementDirection = CalculateMovementDirectionFromCamera();
        characterController.Move(movementDirection * maxMovementSpeed * Time.deltaTime);
        movementVelocity = characterController.velocity;
    }

    private Vector3 CalculateMovementDirectionFromCamera()
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection += Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1) * playerInput.MovementInput.y);
        movementDirection += mainCamera.transform.right * playerInput.MovementInput.x;
        movementDirection.Normalize();
        return movementDirection;
    }

    private void Rotation()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 lookPoint = ray.GetPoint(enter);
            lookDirection = lookPoint - transform.position;
            lookDirection.Normalize();
            transform.forward = lookDirection;
        }
    }
}
