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

        if (Input.GetKey(KeyCode.LeftControl))
        {
            // Cut player height and move camera and groundcheck //
            characterController.height = 0.4f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            // Reset player height and move camera and groundcheck //
            characterController.height = 1f;
        }

        Move();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 10))
            {
                if (hit.transform.CompareTag("BubbleBottle") && !GameManager.Instance.hasBottle)
                {
                    GameManager.Instance.hasBottle = true;
                    Destroy(hit.transform.gameObject);
                }
                else if (hit.transform.CompareTag("BubblePC") && !GameManager.Instance.hasBottle)
                {
                    hit.transform.gameObject.GetComponent<PCBubbleShop>().SpawnBottle();
                }
                else if (hit.transform.CompareTag("BubbleGod") && GameManager.Instance.hasBottle)
                {
                    GameManager.Instance.hasBottle = false;
                    GameManager.Instance.ClickBubble();
                }
            }
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(movementSpeed * Time.deltaTime * move);

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
