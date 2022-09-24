using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;
    [SerializeField] ParticleSystem shatterParticle;
    private GameObject Shattered;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Shattered = Instantiate(destroyedVersion, transform.position, transform.rotation);

            foreach(Rigidbody rb in Shattered.GetComponentsInChildren<Rigidbody>() )
            {
                Vector3 shatterForce = (rb.transform.position - transform.position).normalized * 50;
                rb.AddForce(shatterForce);
            }
            
            //gameObject.SetActive(false);
            MeshRenderer originalMesh = gameObject.GetComponent<MeshRenderer>();
            originalMesh.enabled = false;
            BoxCollider originalCollider = gameObject.GetComponent<BoxCollider>();
            originalCollider.enabled = false;

            Debug.Log(other.gameObject.name + " hit me");

            shatterParticle.Play(); //Play particle
            FindObjectOfType<AudioManager>().Play("explosion");


            ScoreManager.instance.AddPoint(); //Add points

            Invoke("Killme", 5f);
        }
        
    }
    private void Killme()
    {
        Destroy(gameObject);
        Destroy(Shattered);
    }

}
