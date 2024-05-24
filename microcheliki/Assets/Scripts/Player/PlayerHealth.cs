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

    private void Update()
    {
        UpdateHealthBar();  

    }
    public void TakeDamageFromAttack(float damageAmount)
    {
        if (health > 0f)
        {
            health -= damageAmount;
            health = Mathf.Clamp(health, 0, maxHealth);
            isAlive = true;
            UpdateHealthBar();
            Debug.Log(health);
        }
        else if (health == 0f)
        {
            //Die function should be called here
        }
    }


    public void UpdateHealthBar()
    {
        float targetFillAmount = health / maxHealth;
        healthBarFillImage.fillAmount = targetFillAmount;
        healthBarFillImage.DOFillAmount(targetFillAmount, speedUI);

        healthBarFillImage.DOColor(gradientFill.Evaluate(targetFillAmount), speedUI);

    }
}
