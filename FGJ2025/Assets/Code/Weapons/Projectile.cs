using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] int damage = 1;

    public GameObject deathParticles;
    bool bubbledTarget = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnDestroy()
    {
        if (bubbledTarget) return;

	    if (deathParticles != null) 
        {
		    GameObject particles = Instantiate(deathParticles);
		    particles.transform.position = transform.position;
		    particles.transform.rotation = transform.rotation;
	    }
    }

    void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if(health != null && !health.IsBubbled)
        {
            health.TakeDamage(damage);
            bubbledTarget = true;
        }
        
        Destroy(gameObject);
    }
}
