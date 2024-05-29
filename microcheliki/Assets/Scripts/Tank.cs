using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("Car's Fuel")]
    [SerializeField] public int gasVolume = 0;
    [Header("Fuel Capacity")]
    [SerializeField] public int gasVolumeUpperLimit = 100;

    public void Fuel()
    {
        if (gasVolume < gasVolumeUpperLimit) gasVolume = gasVolume + 1;
        else Debug.Log("Car Fuel Is Full!");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            UpgradeTank();
        }
    }

    public void UpgradeTank()
    {
        gasVolumeUpperLimit = gasVolumeUpperLimit + 50;
        // ...
    }

}