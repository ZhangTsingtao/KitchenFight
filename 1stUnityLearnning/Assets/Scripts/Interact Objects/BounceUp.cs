using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceUp : MonoBehaviour
{
    public float BouncePower;
    public Vector3 BounceDir;

    private Rigidbody PlayerRigidBody;

    private void Awake()
    {
        BounceDir = transform. TransformDirection(Vector3.forward);
    }
    void OnTriggerEnter(Collider playercollision)
    {
        if (playercollision.gameObject.CompareTag("Player"))
        {

            PlayerRigidBody = playercollision.GetComponent<Rigidbody>();
            PlayerRigidBody.AddForce(BounceDir * BouncePower, ForceMode.Impulse);
            Debug.Log("Light it up");
        }
    }
}
