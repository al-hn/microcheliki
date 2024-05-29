using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank : MonoBehaviour
{
    [SerializeField] int gasVolume = 100;
    private float decrementInterval = 1.0f;     // Time in sec, between each decrement
    private float nextDecrementTime = 0.0f;
    Tank tank;

    private void Start()
    {
        tank = GameObject.Find("Tank").GetComponent<Tank>();
    }

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.name == "Muscle")
        {
            if(gasVolume > 0)
            {
                gasVolume = gasVolume - 1;
                tank.Fuel();
                nextDecrementTime = Time.time + decrementInterval; // Wait 1 Sec
                Debug.Log($"gasVol: {gasVolume}");
            }
            else
            {
                Debug.Log("GasTankIs Drained!");
            }
        }
    }



}
