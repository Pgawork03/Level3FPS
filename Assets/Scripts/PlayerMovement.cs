using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
 //components
  private CharacterController characterController;
  private Transform cameraTransform;

    //movement and jump configuration paramenters
    [SerializeField] private float speed = 5f;
    [SerializeField] private float multiplier = 2f;
    [SerializeField] private float jumpForce = 1.5f;
    [SerializeField] private float gravity = Physics.gravity.y;

    //Input fields for movement and look actions
    private Vector2 moveInput;
    private Vector2 lookInput;

    //Velocity and rotation variables
    private Vector2 velocity;
    private float verticalVelocity;
    private float verticalRotation = 0;

    //Is Sprinting state
    private bool isSprinting;

    //Camara look sensitivity and max angle to limit vertical rotation
    private float lookSentitivity = 0.5f;
    private float maxLookAngle = 80f;

    private void Start()
    {
            characterController = GetComponent<CharacterController>();
            cameraTransform = Camera.main.transform;
            //hide cursor

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MovePlayer();

        LookAround();
    }


    /// <summary>
    /// /Receives movemnt input from Input System
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    } 
    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)
        {
            //calcule the requite velocity for a jump
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
    /// <summary>
    /// Receive Spreint input from Input System and change isSprinting state
    /// </summary>
    /// <param name="context"></param>
    public void Sprint(InputAction.CallbackContext context)
    {
       isSprinting = context.started || context.performed;
    }

    /// <summary>
    /// Handles player movement based on Input and applies gravity
    /// </summary>
    private void MovePlayer()
    {
        //Jump
        if (characterController.isGrounded)
        {
            //Restart vertical celocity when touch ground
            verticalVelocity = 0f;
        }
        else
        {
            //when is falling down increments velocity with gravity and time
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 move = new Vector3(0, verticalVelocity, 0);
        characterController.Move(move* Time.deltaTime);

        //Movement
        Vector3 MoveDirection= new Vector3(moveInput.x, 0, moveInput.y);
        MoveDirection = transform.TransformDirection(MoveDirection);
        float targetSpeed = isSprinting ? speed * multiplier: speed;
        characterController.Move(MoveDirection * targetSpeed * Time.deltaTime);

        //Apply gravity constantly
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }       
    private void LookAround()
    {
        //Horizontal rotantion
        float horizontalRotation = lookInput.x * lookSentitivity;
        transform.Rotate(Vector3.up * horizontalRotation);

        //Vertical rotation with clamping
        verticalRotation -= lookInput.y * lookSentitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation,0f ,0f);

    }
}
