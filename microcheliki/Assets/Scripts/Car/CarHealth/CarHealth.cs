using System.Collections;
using UnityEngine;

public class CarHealth : MonoBehaviour
{
    [Header("Car Stats")]
    [SerializeField] private float car_MaxHealth = 100f;
    [SerializeField] private float car_health = 100f;

    private void Start()
    {
        car_health = Mathf.Clamp(car_health, 0f, car_MaxHealth);
        Debug.Log("Car health initialized to: " + car_health);
    }

    public void DamageCar(float damageAmount)
    {
        car_health = Mathf.Clamp(car_health - damageAmount, 0f, car_MaxHealth);
        Debug.Log("Car took damage. Current health: " + car_health);
    }

    public void RestoreHealth(float healAmount)
    {
        car_health = Mathf.Clamp(car_health + healAmount, 0f, car_MaxHealth);
        Debug.Log("Car health restored. Current health: " + car_health);
    }

    public void IncreaseMaxHealthCapacity(float increaseAmount)
    {
        car_MaxHealth += increaseAmount;
        car_health = car_MaxHealth;
        Debug.Log("Car max health increased. New max health: " + car_MaxHealth);
    }
}
