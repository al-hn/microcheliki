using UnityEngine;

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
    [Header("Upgrade Properties")]
    public float[] upgradeFireRates;
    public float[] upgradeRanges;
    public int[] upgradeCosts;
    public float[] upgradeBulletSpeeds;
    private int currentUpgradeLevel = 0;

    private FinanceManager financeManager;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 2f);
        financeManager = FindObjectOfType<FinanceManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UpgradeTurret();
        }

        if (targetEnemy != null)
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
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void Shoot()
    {
        Debug.Log("Shoot");
        GameObject bullet_GO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bullet_GO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);

            if (currentUpgradeLevel < upgradeBulletSpeeds.Length)
            {
                bullet.speed = upgradeBulletSpeeds[currentUpgradeLevel];
            }
            else
            {
                Debug.LogWarning("currentUpgradeLevel is out of bounds for upgradeBulletSpeeds array.");
                bullet.speed = upgradeBulletSpeeds[upgradeBulletSpeeds.Length - 1]; // Set to max level speed as fallback
            }
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

    public int GetUpgradeCost()
    {
        if (currentUpgradeLevel < upgradeCosts.Length)
        {
            return upgradeCosts[currentUpgradeLevel];
        }
        return -1; 
    }
    public void UpgradeTurret()
    {
        if (currentUpgradeLevel >= upgradeFireRates.Length || currentUpgradeLevel >= upgradeRanges.Length)
        {
            Debug.Log("Turret is on MAX Level.");
            return;
        }

        int cost = GetUpgradeCost();

        // Check if it's not the first level and if the cost is valid
        if (currentUpgradeLevel > 0 && cost > 0 && financeManager != null)
        {
            if (financeManager.SpendMoney(cost))
            {
                fireRate = upgradeFireRates[currentUpgradeLevel];
                range = upgradeRanges[currentUpgradeLevel];
                currentUpgradeLevel++;

                Debug.Log("Turret upgraded! Fire Rate: " + fireRate + ", Range: " + range);
            }
            else
            {
                Debug.Log("Not enough money to upgrade turret!");
            }
        }
        else
        {
            // If it's the first level or the cost is 0, upgrade without withdrawing money
            fireRate = upgradeFireRates[currentUpgradeLevel];
            range = upgradeRanges[currentUpgradeLevel];
            currentUpgradeLevel++;

            Debug.Log("Turret upgraded! Fire Rate: " + fireRate + ", Range: " + range);
        }
    }




}
