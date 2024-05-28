using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class OutlineObject : MonoBehaviour
{
    [Header("Outline Material")]
    public Material outlineMaterial;
    private Material originalMaterial;
    private MeshRenderer meshRenderer;
    [Header("Detection Distance")]
    public float rayDistance = 10.0f;
    private bool isHighlighted = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    private void HighlightObject()
    {
        if (!isHighlighted)
        {
            meshRenderer.material = outlineMaterial;
            isHighlighted = true;
        }
    }

    private void ClearHighlighted()
    {
        if (isHighlighted)
        {
            meshRenderer.material = originalMaterial;
            isHighlighted = false;
        }
    }

    private void HighlightObjectInCenterOfCam()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit rayHit;
        
        if (Physics.Raycast(ray, out rayHit, rayDistance))
        {
            // Check if the hit object is the same as this object.
            if (rayHit.collider.gameObject == gameObject)
            {
                HighlightObject();
            }
            else
            {
                ClearHighlighted();
            }
        }
        else
        {
            ClearHighlighted();
        }
    }

    private void Update()
    {
        HighlightObjectInCenterOfCam();
    }
}
