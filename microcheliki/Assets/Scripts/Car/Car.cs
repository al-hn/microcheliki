using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Wheel Properties")]
    [SerializeField] AttachingObject[] wheel_attachmentPoints;
    [SerializeField] AttachSpecificObject shieldObject;
    [SerializeField] AttachSpecificObject spikeObject; // Add spike object
    private int attachedWheelsCount = 0;
    [HideInInspector]
    public bool wheelsAttached = false;
    private bool shieldAttachedLastFrame = false;
    private bool spikeAttachedLastFrame = false;
    private bool shieldActivated = true;
    private bool spikeActivated = false;

    private void Start()
    {
        if (wheel_attachmentPoints.Length == 0)
        {
            Debug.LogError("No attachment points found on the car.");
        }
    }

    private void Update()
    {
        if (!wheelsAttached)
        {
            CheckIfWheelsAttached();
        }

        CheckIfShieldOrSpikeIsActivated();
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

    private void CheckIfShieldOrSpikeIsActivated()
    {
        bool shieldCurrentlyAttached = shieldObject.isAttached;
        bool spikeCurrentlyAttached = spikeObject.isAttached;

        // If the shield state has changed
        if (shieldCurrentlyAttached && !shieldAttachedLastFrame)
        {
            if (spikeCurrentlyAttached)
            {
                RemoveSpike();
            }
            Debug.Log("Shield is attached");
            shieldObject.gameObject.SetActive(true); // Enable the shield object
            shieldActivated = true;
            spikeActivated = false;
        }

        // If the spike state has changed
        if (spikeCurrentlyAttached && !spikeAttachedLastFrame)
        {
            if (shieldCurrentlyAttached)
            {
                RemoveShield();
            }
            Debug.Log("Spike is attached");
            spikeObject.gameObject.SetActive(true); // Enable the spike object
            spikeActivated = true;
            shieldActivated = false;
        }

        shieldAttachedLastFrame = shieldCurrentlyAttached;
        spikeAttachedLastFrame = spikeCurrentlyAttached;
    }

    private void RemoveShield()
    {
        Debug.Log("Removing shield");
        shieldObject.gameObject.SetActive(false); // Disable the shield object
        shieldAttachedLastFrame = false;
    }

    private void RemoveSpike()
    {
        Debug.Log("Removing spike");
        spikeObject.gameObject.SetActive(false); // Disable the spike object
        spikeAttachedLastFrame = false;
    }
}
