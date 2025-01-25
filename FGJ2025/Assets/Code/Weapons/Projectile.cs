using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 5f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] int damage = 10;
    int layer;

    /// Game object to spawn when this projectile is destroyed.
    public GameObject deathParticles;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnDestroy()
    {
	    if (deathParticles != null) {
		    GameObject particles = Instantiate(deathParticles);
		    particles.transform.position = transform.position;
		    particles.transform.rotation = transform.rotation;
	    }
    }

    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
    
    public void Initialize(int targetLayer, int dmg, float speed, Vector3 forward, float time = 5f)
    {
        layer = targetLayer;
        lifeTime = time;
        damage = dmg;
        Speed = speed;
        transform.forward = forward;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layer)
        {
            Health health = other.gameObject.GetComponent<Health>();
            if(health)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        //var health = collision.gameObject.GetComponent<Health>();
        //if (health != null)
        //{
        //    health.TakeDamage(damage);
        //}

        //Destroy(gameObject);
    }
}
