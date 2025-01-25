using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    public float Speed = 5f;         // Movement speed
    public float Sprint = 20f;
    public float JumpHeight = 2f;    // Height of the jump
    public float Gravity = -9.81f;   // Gravity force

    private Vector3 velocity;        // For vertical movement
    private bool isGrounded;         // Check if the player is grounded
    public float movementSpeed;
    public bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Check if the player is on the ground
        isGrounded = characterController.isGrounded;

        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep grounded
        }

        // Movement input
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = Sprint;
        }
        else
        {
            movementSpeed = Speed;
        }
        if (Input.GetKey(KeyCodeDown.LeftControl))
        {
            // Cut player height and move camera and groundcheck //
            characterController.height = 0.4f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            // Reset player height and move camera and groundcheck //
            characterController.height = 1f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
       
        characterController.Move(move * Time.deltaTime * movementSpeed);

        // Jumping logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        // Apply gravity to vertical velocity
        velocity.y += Gravity * Time.deltaTime;

        // Move the character based on vertical velocity
        characterController.Move(velocity * Time.deltaTime);

        
      
    }
}
