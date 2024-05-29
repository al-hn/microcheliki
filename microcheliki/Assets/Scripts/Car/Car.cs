using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Wheel Properties")]
    [SerializeField] AttachingObject[] wheel_attachmentPoints;
    [SerializeField] AttachingObject shieldAttachPoint;
    private int attachedWheelsCount = 0;
    private bool wheelsAttached = false;
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
            Debug.Log("All four wheels are attached. Car is ready for driving!");
            wheelsAttached = true;
        }
    }

    private void CheckIfShieldIsActivated()
    {
        if (shieldAttachPoint.isAttached)
        {
            Debug.Log("Shield is attached");
            shield.ActivateShield();
            shieldActivated = true;

        }
    }
}
