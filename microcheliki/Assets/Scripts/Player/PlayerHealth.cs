using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    [Header("HealthProperties")]
    public float health;
    public float maxHealth;
    public bool isAlive;

    [Header("HealthChargeProperties")]
    public int chargingSpeed;

    [Header("HealthBarUI")]
    public Image healthBarFillImage;
    public float speedUI;
    [SerializeField] private Gradient gradientFill;

    [Header("HungerProperties")]
    public float hunger;
    public float maxHunger;
    public float hungerDecreaseRate; 
    public float healthDecreaseFromHungerRate; 

    [Header("HungerBarUI")]
    public Image hungerBarFillImage;
    public float hungerUISpeed;
    [SerializeField] private Gradient hungerGradientFill;

    private void Start()
    {
        isAlive = true;
        hunger = maxHunger;
        StartCoroutine(HungerCoroutine());
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateHungerBar();
    }

    public void TakeDamageFromAttack(float damageAmount)
    {
        if (health > 0f)
        {
            health -= damageAmount;
            health = Mathf.Clamp(health, 0, maxHealth);
            isAlive = health > 0;
            UpdateHealthBar();
            Debug.Log(health);
        }

        if (health == 0f && isAlive)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        Debug.Log("Player has died.");
        
    }

    public void UpdateHealthBar()
    {
        float targetFillAmount = health / maxHealth;
        healthBarFillImage.fillAmount = targetFillAmount;
        healthBarFillImage.DOFillAmount(targetFillAmount, speedUI);
        healthBarFillImage.DOColor(gradientFill.Evaluate(targetFillAmount), speedUI);
    }

    public void UpdateHungerBar()
    {
        float targetFillAmount = hunger / maxHunger;
        hungerBarFillImage.fillAmount = targetFillAmount;
        hungerBarFillImage.DOFillAmount(targetFillAmount, hungerUISpeed);
        hungerBarFillImage.DOColor(hungerGradientFill.Evaluate(targetFillAmount), hungerUISpeed);
    }

    private IEnumerator HungerCoroutine()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(1f); 

            hunger -= hungerDecreaseRate;
            hunger = Mathf.Clamp(hunger, 0, maxHunger);
            UpdateHungerBar();

            if (hunger == 0f)
            {
                health -= healthDecreaseFromHungerRate;
                health = Mathf.Clamp(health, 0, maxHealth);
                UpdateHealthBar();

                if (health == 0f)
                {
                    Die();
                }
            }
        }
    }

    public void EatFood(float foodAmount)
    {
        hunger += foodAmount;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        UpdateHungerBar();
    }
}
