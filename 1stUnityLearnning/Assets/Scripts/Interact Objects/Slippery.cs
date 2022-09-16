using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour
{
    public Collider playerCollider;
    public PhysicMaterial slipperyMaterial;
    public ParticleSystem slipperyParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Yekis");
            playerCollider.material = slipperyMaterial;
            slipperyParticle.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("left the surface");
            //playerCollider.material = null;
            //Debug.Log("Should be back to normal");
            Invoke(nameof(BacktoNormalMaterial), 5f);
        }
    }
    private void BacktoNormalMaterial()
    {
        playerCollider.material = null;
        slipperyParticle.Stop();
        Debug.Log("Should be back to normal");

    }
}
