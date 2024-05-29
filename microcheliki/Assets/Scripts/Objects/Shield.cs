using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [Header("Shield Properties")]
    public float shieldCapacity;
    


    [Header("Upgrade Properties")]
    public float[] upgradeHealthCapacityAmount;
    public int[] upgradeCosts;
    private int currentUpgradeLevel = 0;

    private FinanceManager financeManager;
    private CarHealth carHealth;

    void Start()
    {
        financeManager = FindObjectOfType<FinanceManager>();
        carHealth = FindObjectOfType<CarHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UpgradeShield();
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
    public void UpgradeShield()
    {
        if (currentUpgradeLevel >=  upgradeHealthCapacityAmount.Length)
        {
            Debug.Log("Shield is on MAX Level.");
            return;
        }

        int cost = GetUpgradeCost();

        // Check if it's not the first level and if the cost is valid
        if (currentUpgradeLevel > 0 && cost > 0 && financeManager != null)
        {
            if (financeManager.SpendMoney(cost))
            {
                shieldCapacity = upgradeHealthCapacityAmount[currentUpgradeLevel];
                currentUpgradeLevel++;

                Debug.Log("Shield upgraded!");
            }
            else
            {
                Debug.Log("Not enough money to upgrade shield!");
            }
        }
        else
        {
            shieldCapacity = upgradeHealthCapacityAmount[currentUpgradeLevel];
            currentUpgradeLevel++;
        }
    }

    public void ActivateShield()
    {
        if(carHealth != null)
        {
            carHealth.IncreaseMaxHealthCapacity(shieldCapacity);
            
        }
    }
    //after instantiating shield on car we need to be able to activate shield

}
