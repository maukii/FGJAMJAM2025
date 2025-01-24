using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float damage = 10f;


    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //var health = collision.gameObject.GetComponent<Health>();
        //if (health != null)
        //{
        //    health.TakeDamage(damage);
        //}

        Destroy(gameObject);
    }
}
