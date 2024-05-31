using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] int damage = 1;
    private float decrementInterval = 1.0f;     // Time in sec, between each decrement
    private float nextDecrementTime = 0.0f;

    ZombieEnemy zombie;          // Placeholder
    private void Start()
    {
        zombie = FindAnyObjectByType<ZombieEnemy>();
    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            zombie.GetHitAnim(damage);
            nextDecrementTime = Time.time + decrementInterval; // Wait 1 Sec
        }
    }
}
