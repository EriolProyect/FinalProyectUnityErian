using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControlScript : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 25f;
    [SerializeField]
    private float jumpHeight = 5f;
    [SerializeField]
    private float gravityValue = -29.43f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private InputManager inputManager;
    public Transform cameraTransform;


    private void Awake()
    {
        
    }
    
    private void Start()
    {
        inputManager = InputManager.Instance;
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0 )
        {
            playerVelocity.y = 0;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        Vector3 flatForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
        Vector3 flatRight = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z).normalized;

        move =  flatForward * move.z + flatRight * move.x;

        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (inputManager.PlayerSprint())
        {
            playerSpeed = 50f;
        }
        else
        {
            playerSpeed = 25f;
        }
    }
}
