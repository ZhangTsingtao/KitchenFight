using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceUp : MonoBehaviour
{
    public float BouncePower;
    public Vector3 BounceDir;
    private bool Stepped;
    public float bounceTime = 0.5f;

    private Rigidbody PlayerRigidBody;

    public ParticleSystem dashParticle;

    private void Awake()
    {
        BounceDir = transform. TransformDirection(Vector3.forward);
    }
    void OnTriggerEnter(Collider playercollision)
    {
        if (playercollision.gameObject.CompareTag("Player"))
        {

            PlayerRigidBody = playercollision.GetComponent<Rigidbody>();
            Stepped = true;
            dashParticle.Play();
            //PlayerRigidBody.AddForce(BounceDir * BouncePower, ForceMode.Impulse);
            Debug.Log("Stepped: " + Stepped);

            Invoke("StopBounce", bounceTime);
        }
        
    }

    private void StopBounce()
    {
        Stepped = false;
    }

    private void FixedUpdate()
    {
        if (Stepped)
        {
            PlayerRigidBody.AddForce(BounceDir * BouncePower, ForceMode.Force);
        }
    }
}
