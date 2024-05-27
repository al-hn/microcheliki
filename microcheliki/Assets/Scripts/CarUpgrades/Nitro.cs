using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    private float normalSpeed = 10;
    private float currentSpeed;
    private float carSpeed = 10; // Placeholder For Car's Normal Speed
    [Header("Boost Parameters")]
    public float boostMultiplier = 2.0f;
    public float boostDuration = 2.0f;
    public float boostCooldown = 5.0f;
    [Header("Boost State")]
    public bool isBoosting = false;
    [HideInInspector]
    public float boostEndTime = 0.0f;
    [HideInInspector]
    public float nextBoostTime = 0.0f;

    /*
        On CarController Create new variable current speed,
        and set it to normal speed.
        void Start()
        {
            ...
            ...
            currentSpeed = normalSpeed;
        }
    */

    /*
        In CarController Script Invoke HandleNitroBoost() Inside Update method
        void Update
        {
            ...
            ...
            HandleNitroBoost();
        }
    */

    public void HandleNitroBoost()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > nextBoostTime)
        {
            isBoosting = true;
            boostEndTime = Time.time + boostDuration;
            nextBoostTime = Time.time + boostDuration + boostCooldown;
            currentSpeed = normalSpeed * boostMultiplier;
        }
        if (isBoosting && Time.time > boostEndTime)
        {
            isBoosting = false;
            currentSpeed = normalSpeed;
        }
    }
}
