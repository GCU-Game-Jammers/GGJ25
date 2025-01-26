using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Camera playerCamera;
    private CharacterController characterController;
    public float BaseSpeed = 5f;         // Movement speed
    public float sprintModifier = 1.5f;
    public float crouchModifier = .5f;
    public float slideDecaySpeed = .1f;
    private Vector3 slideDir;
    private float currSlideSpeed;

    private Vector3 velocity;        // For vertical movement
    private bool isGrounded;         // Check if the player is grounded
    public float movementSpeed;
    public bool isCrouched = false;
    private bool isSprinting = false;
    private bool isSliding = false;


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
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, 10))
            {
                if (hit.transform.CompareTag("BubbleBottle") && !GameManager.Instance.hasBottle)
                {
                    GameManager.Instance.hasBottle = true;
                    GameManager.Instance.ClickBubble();
                    Destroy(hit.transform.gameObject);
                }
                else if (hit.transform.CompareTag("BubblePC") && !GameManager.Instance.hasBottle)
                {
                    hit.transform.gameObject.GetComponent<PCBubbleShop>().SpawnBottle();

                }
            }
        }
    }

    private void Move()
    {
        // Gravity 
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (isSliding)
        {
            characterController.Move(slideDir * currSlideSpeed * Time.deltaTime);
        }
        else
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


    private void CalculateSpeed()
    {
        movementSpeed = BaseSpeed;
        
        // Sliding
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.W))
        {
            currSlideSpeed = BaseSpeed;
            if (isSprinting)
                currSlideSpeed *= sprintModifier;
            slideDir = transform.forward;
            isSliding = true;
            transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isSliding = false;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }


        if (isSliding)
        {
            currSlideSpeed -= slideDecaySpeed;
            currSlideSpeed = Mathf.Max(0.0f, currSlideSpeed);
        }
        else
        {
            // Modifiers
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementSpeed *= sprintModifier;
                isSprinting = true;
            }
            else
                isSprinting = false;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                movementSpeed *= crouchModifier;
                transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
                isCrouched = true;
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                isCrouched = false;
            }
        }        
    }

    private void ScottCalculateSpeed()
    {

        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep grounded
        }
        if (isCrouched && isSliding == false)
        {
            movementSpeed = CrouchSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && isSliding == false && isCrouched == false)
        {
            movementSpeed = Sprint;
            isSprinting = true;
        }
        else if (isSliding == false)
        {
            movementSpeed = BaseSpeed;
            isSprinting = false;
        }
        // Movement input

        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched == false)
        {
            if (isSprinting == true && isSliding == false)
            {
                isSliding = true;
                currentSlideSpeed = slideSpeed;
                transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
                isCrouched = true;
            }
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouched == true && isSliding == false)
        {
            transform.localScale = new Vector3(1.0f, 1f, 1.0f);
            isCrouched = false;
        }

        if (isSliding == true)
        {
            movementSpeed = currentSlideSpeed;
            currentSlideSpeed -= slideDecayRate * Time.deltaTime;

            if (currentSlideSpeed <= BaseSpeed)
            {
                isSliding = false;
                isCrouched = true;
            }
        }
    }
}
