using UnityEngine;
using System;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    #region Properties

    [SerializeField]
    private float _maxAcceleration;

    [SerializeField]
    private float _brakeForce;

    [SerializeField]
    private float _turnSensitivity;

    [SerializeField]
    private float _maxSteerAngle;

    [SerializeField]
    private Vector3 _centerOfMass;

    [SerializeField]
    private List<Wheel> _wheels;

    #endregion

    public float MaxAcceleration { get => _maxAcceleration; set => _maxAcceleration = value; }
    public float BrakeForce { get => _brakeForce; set => _brakeForce = value; }
    public float TurnSensitivity { get => _turnSensitivity; set => _turnSensitivity = value; }
    public float MaxSteerAngle { get => _maxAcceleration; set => _maxSteerAngle = value; }
    public Vector3 CenterOfMass { get => _centerOfMass; set => _centerOfMass = value; }
    public List<Wheel> wheels { get => _wheels; set => _wheels = value; }

    // used internally
    float _accelerationInput;
    float _steerInput;
    private Rigidbody _car;
    private SensorSystem _sensors;

    void Start()
    {
        _car = GetComponent<Rigidbody>();
        _car.centerOfMass = _centerOfMass;

         _sensors = GetComponent<SensorSystem>();
    }

    public void Re(float a, float b)
    {
        GetInputs(a, b);
        AnimateWheels();
    }

    void LateUpdate()
    {
        _sensors.Sensors();
        Move();
        Steer();
        Brake();
    }

    void GetInputs(float? moveInput = null, float? steerInput = null)
    {
        if(moveInput.HasValue && steerInput.HasValue)
        {
            _accelerationInput = moveInput.Value;
            _steerInput = steerInput.Value;
        }
    }

    void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = _accelerationInput * 600 * _maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = _steerInput * _turnSensitivity * _maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || _accelerationInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * _brakeForce * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    void AnimateWheels()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}