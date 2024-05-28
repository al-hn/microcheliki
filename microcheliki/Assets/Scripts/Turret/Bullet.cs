using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    public float explosionRadius = 0f;
    private bool targetHit;
    private Transform target;
    public GameObject explosionParticle;
    public float speed = 70f;

    private void Start()
    {
        // Initialize Rigidbody if necessary
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // Play explosion effect
        GameObject effectInstance = Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(effectInstance, 10f); 

        
        Damage(target);

        // Destroy the bullet
        Destroy(gameObject,5f);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void Damage(Transform enemy)
    {
        ZombieEnemy e = enemy.GetComponent<ZombieEnemy>();
        if (e != null)
        {
            e.GetHitAnim(20);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
        {
            return;
        }

        // Check if the collision is with the target
        if (collision.transform == target)
        {
            targetHit = true;
            HitTarget();
        }
      
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }
}
