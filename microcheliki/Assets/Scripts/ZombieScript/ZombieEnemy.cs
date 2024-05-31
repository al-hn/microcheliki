using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : MonoBehaviour
{
    private GameObject[] players;
    private GameObject[] cars;
    private Rigidbody myBody;
    private Animator anim;

    [Header("Enemy Attack Properties")]
    public float enemy_Watch_Treshold = 10f;
    public float enemy_Attack_Treshold = 5f;

    public float destroyDelayTime = 0.5f;

    [Header("Stats")]
    public float health;
    private float enemy_Speed;
    public float enemy_start_Speed;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        EnemyAI();
    }

    private void Start()
    {
        enemy_Speed = enemy_start_Speed;
    }

    void EnemyAI()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        cars = GameObject.FindGameObjectsWithTag("Car");

        if (players.Length == 0 && cars.Length == 0) return;

        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (var player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = player;
            }
        }

        foreach (var car in cars)
        {
            float distance = Vector3.Distance(transform.position, car.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = car;
            }
        }

        if (closestTarget == null) return;

        Vector3 direction = closestTarget.transform.position - transform.position;
        float distanceToTarget = direction.magnitude;
        direction.Normalize();

        Vector3 velocity = direction * enemy_Speed;

        if (distanceToTarget > enemy_Attack_Treshold && distanceToTarget < enemy_Watch_Treshold)
        {
            myBody.velocity = new Vector3(velocity.x, myBody.velocity.y, velocity.z);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stop"))
            {
                anim.SetTrigger("Stop");
            }

            anim.SetTrigger("Run");

            transform.LookAt(new Vector3(closestTarget.transform.position.x,
                transform.position.y, closestTarget.transform.position.z));
        }
        else if (distanceToTarget < enemy_Attack_Treshold)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                anim.SetTrigger("Stop");
            }

            anim.SetTrigger("Attack");

            transform.LookAt(new Vector3(closestTarget.transform.position.x,
                transform.position.y, closestTarget.transform.position.z));

            // Deal damage to the target
            if (closestTarget.CompareTag("Player"))
            {
                PlayerHealth playerHealth = closestTarget.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamageFromAttack(10f); 
                }
            }
            else if (closestTarget.CompareTag("Muscle"))
            {
                CarHealth carHealth = closestTarget.GetComponent<CarHealth>();
                if (carHealth != null)
                {
                    carHealth.DamageCar(10f); 
                }
            }
        }
        else
        {
            myBody.velocity = Vector3.zero;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.SetTrigger("Stop");
            }
        }

        enemy_Speed = enemy_start_Speed;
    }

    public void GetHitAnim(float damage)
    {
        enemy_Speed = 0f;
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}



