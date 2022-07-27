using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorSystem : MonoBehaviour
{
    #region Property Fields
    private float _sensorLength = 25;
    private float _frontSensorOffset;
    private float _sideSensorOffset;

    [SerializeField]
    private float _basicSensorAngle;
    [SerializeField]
    private float _steepSensorAngle;
    [SerializeField]
    private float _sideSensorAngle;

    #endregion

    public float SensorLength { get => _sensorLength; set => _sensorLength = value; }
    public float FrontSensorOffset { get => _frontSensorOffset; set => _frontSensorOffset = value; }
    public float SideSensorOffset { get => _sideSensorOffset; set => _sideSensorOffset = value; }
    public float BasicSensorAngle { get => _basicSensorAngle; set => _basicSensorAngle = value; }
    public float SteepSensorAngle { get => _steepSensorAngle; set => _steepSensorAngle = value; }
    public float SideSensorAngle { get => _sideSensorAngle; set => _sideSensorAngle = value; }

    private int _layerMask;

    public void Awake()
    {
        _layerMask = LayerMask.GetMask("SensorLayer");
    }

    public void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartingPosition;
        var hitDistance = 0.1f;


        sensorStartingPosition = transform.position;
        sensorStartingPosition += transform.forward * 1.85f;
        sensorStartingPosition.y += 0.5f;
        // front center sensor
        if(Physics.Raycast(sensorStartingPosition, transform.forward, out hit, _sensorLength, _layerMask))
        {
            if(hit.distance < hitDistance)
                Debug.Log("Hit detected!!!");

            Debug.DrawLine(sensorStartingPosition, hit.point, Color.green);
        }

        sensorStartingPosition = transform.position;
        sensorStartingPosition += transform.forward * 1.85f;
        sensorStartingPosition.y += 0.5f;
        sensorStartingPosition += transform.right * 0.75f;
        // front right sensor
        if(Physics.Raycast(sensorStartingPosition, Quaternion.AngleAxis(BasicSensorAngle, transform.up) * transform.forward, out hit, _sensorLength, _layerMask))
        {
                        if(hit.distance < hitDistance)
                Debug.Log("Hit detected!!!");
            Debug.DrawLine(sensorStartingPosition, hit.point, Color.green);
        }

        // front left sensor
        sensorStartingPosition = transform.position;
        sensorStartingPosition += transform.forward * 1.85f;
        sensorStartingPosition.y += 0.5f;
        sensorStartingPosition += transform.right * (-0.75f);
        if(Physics.Raycast(sensorStartingPosition, Quaternion.AngleAxis(-BasicSensorAngle, transform.up) * transform.forward, out hit, _sensorLength, _layerMask))
        {
                        if(hit.distance < hitDistance)
                Debug.Log("Hit detected!!!");
            Debug.DrawLine(sensorStartingPosition, hit.point, Color.green);            
        }

        sensorStartingPosition = transform.position;
        sensorStartingPosition += transform.forward * 1.0f;
        sensorStartingPosition.y += 0.5f;
        sensorStartingPosition += transform.right * 0.75f;
        // front right sensor 2
        if(Physics.Raycast(sensorStartingPosition, Quaternion.AngleAxis(SteepSensorAngle, transform.up) * transform.forward, out hit, _sensorLength, _layerMask))
        {
                        if(hit.distance < hitDistance)
                Debug.Log("Hit detected!!!");
            Debug.DrawLine(sensorStartingPosition, hit.point, Color.green);
        }

        // front left sensor 2
        sensorStartingPosition = transform.position;
        sensorStartingPosition += transform.forward * 1.0f;
        sensorStartingPosition.y += 0.5f;
        sensorStartingPosition += transform.right * (-0.75f);
        if(Physics.Raycast(sensorStartingPosition, Quaternion.AngleAxis(-SteepSensorAngle, transform.up) * transform.forward, out hit, _sensorLength, _layerMask))
        {
                        if(hit.distance < hitDistance)
                Debug.Log("Hit detected!!!");
            Debug.DrawLine(sensorStartingPosition, hit.point, Color.green);            
        }

        sensorStartingPosition = transform.position;
        sensorStartingPosition.y += 0.5f;
        sensorStartingPosition += transform.right * 0.75f;
        // right side sensor
        if(Physics.Raycast(sensorStartingPosition, Quaternion.AngleAxis(SideSensorAngle, transform.up) * transform.forward, out hit, _sensorLength, _layerMask))
        {
                        if(hit.distance < hitDistance)
                Debug.Log("Hit detected!!!");
            Debug.DrawLine(sensorStartingPosition, hit.point, Color.green);
        }

        sensorStartingPosition = transform.position;
        sensorStartingPosition.y += 0.5f;
        sensorStartingPosition += transform.right * 0.75f;
        
        // right side sensor
        if(Physics.Raycast(sensorStartingPosition, Quaternion.AngleAxis(SideSensorAngle, transform.up) * transform.forward, out hit, _sensorLength, _layerMask))
        {
                        if(hit.distance < hitDistance)
                Debug.Log("Hit detected!!!");
            Debug.DrawLine(sensorStartingPosition, hit.point, Color.green);
        }

        // left side sensor
        sensorStartingPosition = transform.position;
        sensorStartingPosition.y += 0.5f;
        sensorStartingPosition += transform.right * (-0.75f);
        if(Physics.Raycast(sensorStartingPosition, Quaternion.AngleAxis(-SideSensorAngle, transform.up) * transform.forward, out hit, _sensorLength, _layerMask))
        {
                        if(hit.distance < hitDistance)
                Debug.Log("Hit detected!!!");
            Debug.DrawLine(sensorStartingPosition, hit.point, Color.green);            
        }
    }
}
