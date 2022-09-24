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

    public Vector3 moveDirection;

    public Rigidbody rb;
    
    public ParticleSystem JumpParticle;
    public ParticleSystem Dust;
    Quaternion particlerotation = Quaternion.identity;
    Vector3 dashDirection;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = 2;

    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);


        MyInput();
        SpeedControl();
        Checkit();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        JumpHeightVariable();
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKeyDown(jumpKey) && readyToJump > 1)
        {
            readyToJump --;
            Jump();
        }

        if (grounded)
            ResetJump();//Invoke(nameof(ResetJump), jumpCooldown);

        //Dash
        if (Input.GetKey(KeyCode.Q))
            Dash();

    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
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

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse );

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
                rb.AddForce(transform.up * jumpForce, ForceMode.Force);
                jumpTimeCounter -= Time.deltaTime;
                //Debug.Log("still holding space, jumptime left " + jumpTimeCounter);
            }
            else
            {
                isJumping = false;
                //Debug.Log("should not be jumping, jumptime left " + jumpTimeCounter);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            //Debug.Log("space key up, jumptime left " + jumpTimeCounter);
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

    private void Checkit()
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