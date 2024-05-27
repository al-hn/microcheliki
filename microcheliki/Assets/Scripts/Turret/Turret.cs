using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
    [Header("Rotation Properties")]

    public float range = 15f;
    public Transform partToRotate;
    public float turnSpeed = 0.5f;

    [Header("Target Properties")]
    public Transform target;
    public ZombieEnemy targetEnemy;

    [Header("Fire Properties")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    
    public ParticleSystem impactEffect;
    public float slowPercentage = .5f;

    

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 2f);
        
    }

    private void Update()
    {
        if(targetEnemy != null) 
        {
            if (fireCountdown <= 0f)
            {
                Shoot();

                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
            LockOnEnemy();

        }
      
        


    }



    private void LockOnEnemy()
    {
        //calculating distance
        Vector3 direction = target.position - transform.position;
        //calculating look at rotation
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //converting to euler angles
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void Shoot()
    {
        Debug.Log("Shoot");
        GameObject bullet_GO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bullet_GO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<ZombieEnemy>();

            }
            else
            {
                target = null;
            }
        }
    }
}