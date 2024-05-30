using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Wheel Properties")]
    [SerializeField] AttachingObject[] wheel_attachmentPoints;
    [SerializeField] AttachSpecificObject shieldObject;
    private int attachedWheelsCount = 0;
    [HideInInspector]
    public bool wheelsAttached = false;
    private bool shieldActivated = false;

    Shield shield;

    private void Start()
    {

        if (wheel_attachmentPoints.Length == 0)
        {
            Debug.LogError("No attachment points found on the car.");
        }
        shield = FindAnyObjectByType<Shield>();

    }

    // Check if all four wheels are attached
    private void Update()
    {
        if (!wheelsAttached)
        {
            CheckIfWheelsAttached();

        }
        if (!shieldActivated)
        {
            CheckIfShieldIsActivated();
        }
        else { return; }


    }

    private void CheckIfWheelsAttached()
    {
        int totalAttachedWheels = 0;
        foreach (var attachmentPoint in wheel_attachmentPoints)
        {
            if (attachmentPoint.isAttached)
            {
                totalAttachedWheels++;
            }
        }

        if (totalAttachedWheels == 4)
        {
            Debug.Log("All four wheels are attached!");
            wheelsAttached = true;
        }
    }

    private void CheckIfShieldIsActivated()
    {
        if (shieldObject.isAttached)
        {
            Debug.Log("Shield is attached");
            shield.ActivateShield();
            shieldActivated = true;

        }
    }
}
