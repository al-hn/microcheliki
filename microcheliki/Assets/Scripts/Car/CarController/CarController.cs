using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformFR;
    [SerializeField] private Transform _transformBL;
    [SerializeField] private Transform _transformBR;

    [SerializeField] private WheelCollider _colliderFL;
    [SerializeField] private WheelCollider _colliderFR;
    [SerializeField] private WheelCollider _colliderBL;
    [SerializeField] private WheelCollider _colliderBR;

    [SerializeField] public float _force;
    [SerializeField] public float _maxAngle;

    [Header("Capsule Colliders")]
    [SerializeField] private GameObject capsuleFL;
    [SerializeField] private GameObject capsuleFR;
    [SerializeField] private GameObject capsuleBL;
    [SerializeField] private GameObject capsuleBR;
    
    [HideInInspector] public bool scriptIsActive;

    private void Awake()
    {
        IgnoreCapsuleColliders();
    }

    private void FixedUpdate()
    {
        if (!scriptIsActive)
            return;
        
        _colliderFL.motorTorque = Input.GetAxis("Vertical") * _force;
        _colliderFR.motorTorque = Input.GetAxis("Vertical") * _force;

        if (Input.GetKey(KeyCode.Space))
        {
            _colliderFL.brakeTorque = 3000f;
            _colliderFR.brakeTorque = 3000f;
            _colliderBL.brakeTorque = 3000f;
            _colliderBR.brakeTorque = 3000f;
        }
        else
        {
            _colliderFL.brakeTorque = 0f;
            _colliderFR.brakeTorque = 0f;
            _colliderBL.brakeTorque = 0f;
            _colliderBR.brakeTorque = 0f;
        }

        _colliderFL.steerAngle = _maxAngle * Input.GetAxis("Horizontal");
        _colliderFR.steerAngle = _maxAngle * Input.GetAxis("Horizontal");

        RotateWheel(_colliderFL, _transformFL);
        RotateWheel(_colliderFR, _transformFR);
        RotateWheel(_colliderBL, _transformBL);
        RotateWheel(_colliderBR, _transformBR);
    }

    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.rotation = rotation;
        transform.position = position;
    }

    public void MotorUpd()
    {
        _force += 1500;
    }

    private void IgnoreCapsuleColliders()
    {
        capsuleFL.layer = LayerMask.NameToLayer("IgnoreCarCollision");
        capsuleFR.layer = LayerMask.NameToLayer("IgnoreCarCollision");
        capsuleBL.layer = LayerMask.NameToLayer("IgnoreCarCollision");
        capsuleBR.layer = LayerMask.NameToLayer("IgnoreCarCollision");

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("IgnoreCarCollision"), gameObject.layer);
    }

    public void ActivateScript()   => scriptIsActive = true;
    public void DeactivateScript() => scriptIsActive = false;
}