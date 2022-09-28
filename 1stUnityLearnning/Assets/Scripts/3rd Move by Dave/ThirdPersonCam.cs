using Cinemachine;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    public float rotationSpeed;

    public float mouseSpeed = 1f;

    public CinemachineFreeLook freelookCam;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        freelookCam.m_XAxis.m_MaxSpeed = PlayerPrefs.GetFloat("MouseSen") * 2 + 100f;
        freelookCam.m_YAxis.m_MaxSpeed = PlayerPrefs.GetFloat("MouseSen") / 50f + 1f;
    }

    private void FixedUpdate()
    {
        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // roate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
            player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }

}
