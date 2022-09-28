using UnityEngine;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public int readyToJump = 2;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    bool checkitground;
    bool isJumping;
    public float jumpTime;
    float jumpTimeCounter;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    public float rotateSen;

    public Vector3 moveDirection;

    public Rigidbody rb;
    
    public ParticleSystem JumpParticle;
    public ParticleSystem Dust;
    Quaternion particlerotation = Quaternion.identity;
    Vector3 dashDirection;

    private bool fuDash;
    public bool fuJump;
    private bool fuJumpHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = 2;

        if (PlayerPrefs.HasKey("MouseSen"))
            rotateSen = PlayerPrefs.GetFloat("MouseSen") * 0.01f;
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
            MyInput();
        CheckItGround();

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
        
        if (fuDash)
            Dash();
        
        if (fuJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        
        if (fuJumpHeight)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Force);
            //Debug.Log(jumpTimeCounter);
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jump
        if (Input.GetKeyDown(jumpKey) && readyToJump > 1)
        {
            readyToJump--;
            Jump();
        }

        if (grounded)
            ResetJump();

        //Dash
        if (Input.GetKey(KeyCode.Q))
            fuDash = true;
        else
            fuDash = false;

        JumpHeightVariable();
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
        //rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

        // in air
        else if(!grounded)
            rb.AddForce(10f * airMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Force);
    }

    private void SpeedControl()
    {
        //Debug.Log(rb.velocity.magnitude);
        Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {

            //Vector3 limitedVel = flatVel.normalized * moveSpeed;
            Vector3 limitedVel = Vector3.ClampMagnitude(flatVel, moveSpeed);
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            
        }
    }

    private void Jump()
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;

        fuJump = true;

        //Play Audio
        FindObjectOfType<AudioManager>().Play("jump");

        //Play Particle
        if (!grounded)
        {
            particlerotation.eulerAngles = new Vector3(90, 0, 0);
            JumpParticle.transform.rotation = particlerotation;
            JumpParticle.Play();
        }
    }

    private void JumpHeightVariable()
    {
        //jump height variable test
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            //Debug.Log("Awake");
            if (jumpTimeCounter > 0)
            {
                fuJumpHeight = true;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            { 
                isJumping = false;
                fuJump = false;
                fuJumpHeight = false;
                Debug .Log("Shouldn't be jumping");
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            fuJump = false;
            fuJumpHeight = false;
        }
    }

    private void Dash()
    {
        dashDirection = JumpParticle.transform.position - orientation.transform.position;
        dashDirection = new Vector3(dashDirection.x, 0, dashDirection.z);
        rb.AddForce(dashDirection.normalized * jumpForce, ForceMode.Impulse );
        Debug.Log("dash direction: " + dashDirection);

        //particle
        JumpParticle.transform.localRotation = Quaternion.Euler(180, 0, 0);
        JumpParticle.Play();
        Debug.Log("particle direction: " + JumpParticle.transform.rotation);

    }
    private void ResetJump()
    {
        readyToJump = 2;
    }

    private void CheckItGround()
    {
        if(checkitground != grounded)
        {
            checkitground = grounded;
            if (grounded)
            {
                Dust.Play();
                FindObjectOfType<AudioManager>().Play("land");
            }
        }

    }
}