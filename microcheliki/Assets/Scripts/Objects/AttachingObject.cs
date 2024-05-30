using UnityEngine;

public class AttachingObject : MonoBehaviour
{
    [Header("Car Properties")]
    [SerializeField] private string itemTag;

    [Header("Wheel Properties")]
    [SerializeField] private GameObject wheelPrefab;
    [SerializeField] private Transform attachPoint;

    [Header("Attachment Properties")]
    [SerializeField] private float lerpSpeed = 10.0f;

    [HideInInspector] public bool isAttached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(itemTag) && !isAttached)
        {
            Debug.Log("Collided with car: " + itemTag);

            // Instantiate the wheel prefab at the attach point position
            GameObject wheelObject = Instantiate(wheelPrefab, attachPoint.position, Quaternion.identity);

            // Set the parent of the wheel object to the attach point
            wheelObject.transform.parent = attachPoint;

            // Disable the collider of the wheel object to prevent it from interacting with physics
            Collider wheelCollider = wheelObject.GetComponent<Collider>();
            if (wheelCollider != null)
            {
                wheelCollider.enabled = false;
            }

            isAttached = true;

            // Destroy the collided object
            Destroy(other.gameObject);
        }
    }
}




