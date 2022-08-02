using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float speed = 5f;
    public float JumpForce = 3f;

    public float CheckDistance = 0.2f;
    public Transform GroundCheck;
    public LayerMask GroundMask;

    public Transform cam;

    public bool canJump;
    public bool canMove;

    public Rigidbody rb;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 moveDir;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = gameObject.GetComponent<Rigidbody>();

    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        //Jump
        canJump = Physics.CheckSphere(GroundCheck.position, CheckDistance, GroundMask);

        if (canJump && Input.GetButton("Jump"))
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        if (canJump && rb.velocity.y < 0)//characterRig.velocity.y
        {
            rb.velocity = new Vector3(0, -2f, 0);
        }

        //Walk
        if (direction.magnitude >= 0.1f)
        {
            moveDir.y = rb.velocity.y;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //moveDir.y = rb.velocity.y;
            rb.velocity = moveDir.normalized * speed;// controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
    }
}    