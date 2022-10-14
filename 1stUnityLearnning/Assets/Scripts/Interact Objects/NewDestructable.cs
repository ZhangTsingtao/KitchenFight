using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDestructable : MonoBehaviour
{
    public GameObject ExplodeParticle;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            //Debug.Log(child.name);
            child.gameObject.AddComponent<MeshCollider>();
            child.gameObject.GetComponent<MeshCollider>().convex = true;
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.GetComponent<Rigidbody>().mass = 0.01f;
            child.gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MeshRenderer originalMesh = gameObject.GetComponent<MeshRenderer>();
            originalMesh.enabled = false;
            BoxCollider originalCollider = gameObject.GetComponent<BoxCollider>();
            originalCollider.enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            foreach (Transform child in transform)
            {
                //Debug.Log(child.name);
                child.gameObject.SetActive(true);
                
            }

            GameObject e = Instantiate(ExplodeParticle, transform.position + Vector3.up, transform.rotation);
            e.GetComponent<ParticleSystem>().Play();
            Destroy(e.gameObject, 2f);


            FindObjectOfType<AudioManager>().Play("explosion");

            ScoreManager.instance.AddPoint();

            SettleDown();
        }

    }
    public void SettleDown()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(7f);
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            child.gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
