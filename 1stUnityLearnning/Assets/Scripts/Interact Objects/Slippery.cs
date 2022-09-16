using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour
{
    public Collider playerCollider;
    public PhysicMaterial slipperyMaterial;
    public ParticleSystem slipperyParticle;
    public float slipperyTime = 7f;

    public bool beeninthePool;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Yekis");
            playerCollider.material = slipperyMaterial;
            slipperyParticle.Play();
            beeninthePool = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && beeninthePool)
        {
            Debug.Log("left the surface");
            //playerCollider.material = null;
            //Debug.Log("Should be back to normal");
            Invoke(nameof(BacktoNormalMaterial), slipperyTime);
        }
    }
    private void BacktoNormalMaterial()
    {
        if (beeninthePool)
        {
            playerCollider.material = null;
            slipperyParticle.Stop();
            beeninthePool = false;
            Debug.Log("Should be back to normal");
        }

    }
}
