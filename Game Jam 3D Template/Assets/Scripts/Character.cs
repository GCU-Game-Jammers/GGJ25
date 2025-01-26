using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    public float BaseSpeed = 5f;         // Movement speed
    public float sprintModifier = 1.5f;
    public float crouchModifier = .5f;
    
    private Vector3 velocity;        // For vertical movement
    private bool isGrounded;         // Check if the player is grounded
    public float movementSpeed;
    public bool crouch = false;

    private bool isSliding = false;
    private bool sprintCheck = false;

    [Header("Old Stuff Can Ignore")]
    public float Sprint = 20f;
    public float CrouchSpeed = 4f;
    public float slideSpeed = 20f;
    public float slideDecayRate = 5f;
    private float currentSlideSpeed;
    public float JumpHeight = 2f;    // Height of the jump
    public float Gravity = -9.81f;   // Gravity force

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

        CalculateSpeed();
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
        // Gravity 
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

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


    private void CalculateSpeed()
    {
        movementSpeed = BaseSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed *= sprintModifier;
            sprintCheck = true;
        }
        else
            sprintCheck = false;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movementSpeed *= crouchModifier;
            transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            crouch = true;
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            crouch = false;
        }
    }

    private void ScottCalculateSpeed()
    {

        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep grounded
        }
        if (crouch && isSliding == false)
        {
            movementSpeed = CrouchSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && isSliding == false && crouch == false)
        {
            movementSpeed = Sprint;
            sprintCheck = true;
        }
        else if (isSliding == false)
        {
            movementSpeed = BaseSpeed;
            sprintCheck = false;
        }
        // Movement input

        if (Input.GetKeyDown(KeyCode.LeftControl) && crouch == false)
        {
            if (sprintCheck == true && isSliding == false)
            {
                isSliding = true;
                currentSlideSpeed = slideSpeed;
                transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
                crouch = true;
            }
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl) && crouch == true && isSliding == false)
        {
            transform.localScale = new Vector3(1.0f, 1f, 1.0f);
            crouch = false;
        }

        if (isSliding == true)
        {
            movementSpeed = currentSlideSpeed;
            currentSlideSpeed -= slideDecayRate * Time.deltaTime;

            if (currentSlideSpeed <= BaseSpeed)
            {
                isSliding = false;
                crouch = true;
            }
        }
    }
}
