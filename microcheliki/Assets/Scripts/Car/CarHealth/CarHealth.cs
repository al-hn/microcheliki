using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealth : MonoBehaviour
{
    [Header("Car Stats")]
    [SerializeField] private float car_MaxHealth;
    [SerializeField] private float car_health;
    


    private void Start()
    {
        car_health = Mathf.Clamp(car_health, 0f, car_MaxHealth);
        Debug.Log(car_health);

    }

    public void DamageCar(float damageAmount)
    {
        car_health -= damageAmount;

    }

    public void RestoreHealth(float healAmount)
    {
        car_health += healAmount;
    }

    public void IncreaseMaxHealthCapacity(float increaseAmount)
    {
        car_MaxHealth += increaseAmount;
    }


}