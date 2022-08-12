using UnityEngine;
public class Coin : MonoBehaviour {

    [SerializeField] ParticleSystem collectParticle;
    [SerializeField] MeshRenderer coinRenderer;
    private bool once = true;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && once)
        {
            ScoreManager.instance.AddPoint();
            Debug.Log(other.name + " ≈ˆµΩ¡ÀŒ“");

            //var em = collectParticle.emission;
            var dur = collectParticle.main.duration;
            //em.enabled = true;
            Debug.Log("coin duration: " + dur);

            collectParticle.Play();

            once = false;
            Destroy(coinRenderer);
            Invoke(nameof(DestoryObj), dur);
        }
        
          
    }
    private void DestoryObj()
    {
        Destroy(gameObject);
    }
}