using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;
    //[SerializeField] ParticleSystem shatterParticle;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //shatterParticle.Play(); //Play particle

            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
            Debug.Log(other.gameObject.name + " hit me");
            
            ScoreManager.instance.AddPoint(); //Add points



        }

    }

}
