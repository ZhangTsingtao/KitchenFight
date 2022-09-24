using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour
{
    public Collider playerCollider;
    public PhysicMaterial slipperyMaterial;
    public ParticleSystem slipperyParticle;
    public ParticleSystem GroundWetParticle;

    public float slipperyTime = 7f;
    float timerTime;
    bool timerOn = false;


    private void Update()
    {
        if (timerOn)
        {
            if(timerTime > 0)
            {
                timerTime -= Time.deltaTime;
            }
            else
            {
                BacktoNormalMaterial();
                timerOn = false;
                timerTime = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Playwet();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Yekis");
            playerCollider.material = slipperyMaterial;

            slipperyParticle.Play();
            GroundWetParticle.Play();

            timerOn = false;
            timerTime = slipperyTime;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            timerOn = true;
            timerTime = slipperyTime;
            Debug.Log("Should reset time to " + timerTime);
        }
    }
    private void BacktoNormalMaterial()
    {
        Debug.Log("Back to Normal!!");
        playerCollider.material = null;
        slipperyParticle.Stop();
        GroundWetParticle.Stop();
    }

    private void Playwet()
    {
        if(timerOn)
            FindObjectOfType<AudioManager>().Play("wet");
    }
}
