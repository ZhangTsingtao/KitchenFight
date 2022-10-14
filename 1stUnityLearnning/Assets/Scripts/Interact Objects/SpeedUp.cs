using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public ParticleSystem dashParticle;
    public ParticleSystem speedParticle;

    private float originalSpeed;
    bool timerOn;
    float timerTime = 5f;
    public bool alrealdyUp = false;
    private void Start()
    {
        originalSpeed = PlayerMovementTutorial.moveSpeed;
    }
    private void Update()
    {
        if (timerOn)
        {
            if (timerTime > 0)
            {
                timerTime -= Time.deltaTime;
            }
            else
            {
                BacktoOrigianSpeed();
                timerOn = false;
                timerTime = 0f;
            }
        }
    }
    void OnTriggerEnter(Collider playercollision)
    {
        if (playercollision.gameObject.CompareTag("Player"))
        {
            timerOn = true;
            timerTime = 5f;
            if (!alrealdyUp)
            {
                PlayerMovementTutorial.moveSpeed *= 1.5f;
                alrealdyUp = true;
                //Debug.Log("upupupup");
            }


            dashParticle.Play();
            speedParticle.Play();
            FindObjectOfType<AudioManager>().Play("bounceUp");

        }
        
    }
    void BacktoOrigianSpeed()
    {
        PlayerMovementTutorial.moveSpeed = originalSpeed;
        speedParticle.Stop();
        alrealdyUp = false;
        //Debug.Log("Done");
    }

}
