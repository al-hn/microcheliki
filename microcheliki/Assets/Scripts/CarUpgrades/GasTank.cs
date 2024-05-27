using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank : MonoBehaviour
{
    [SerializeField] int gasVolume = 100;
    private float decrementInterval = 1.0f;     // Time in sec, between each decrement
    private float nextDecrementTime = 0.0f;

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.name == "Car")
        {
            gasVolume = Mathf.Clamp(gasVolume, 0, 100); // Clamp the value between 0 - 100
            gasVolume = gasVolume - 1;
            nextDecrementTime = Time.time + decrementInterval; // Wait 1 Sec

            // Implement so that car's tank will be fueling
        }
    }



}
