using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    private bool attached = true;   // Placeholder
                                    // Implement Attachment To Car
    private int carHealth = 100;    // Placeholder
    public int protection = 100;
    
    private void Update()
    {
        if (attached)
        {
            carHealth += protection; // Add Protection When Armor is Attached to the car
        }
    }
}
