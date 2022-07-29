using System.Collections.Generic;
using UnityEngine;

public class SensorSystem : MonoBehaviour
{
    #region Property Fields

    [SerializeField]
    [Tooltip("How far the sensor shoots")]
    private float _sensorLength = 25;

    [SerializeField]
    [Tooltip("Threshol for detecting if an object hit something using raycasts")]
    private float _hitThreshold = 0.07f;

    [SerializeField]
    [Tooltip("How much sensors are lifted from the ground")]
    private float _verticalOffset;

    [SerializeField]
    [Tooltip("x -> forward offset, y -> right offset, z -> sensor angle")]
    private List<Vector3> _sensors;

    [SerializeField]
    [Tooltip("What layers will be detected by the sensor")]
    private List<string> _detectableLayers;

    #endregion

    public float SensorLength { get => _sensorLength; set => _sensorLength = value; }
    public float HitDistance { get => _hitThreshold; set => _hitThreshold = value; }
    public float VerticalOffset { get => _verticalOffset; set => _verticalOffset = value; }
    public List<Vector3> Sensorss { get => _sensors; set => _sensors = value; }
    public List<string> DetectableLayers { get => _detectableLayers; set => _detectableLayers = value; }


    private int _layerMask; // calculated using the 'DetectableLayers' property


    public void Awake() =>
        _layerMask = LayerMask.GetMask(DetectableLayers.ToArray());


    // since Unity doesn't support transform.left, to supply the left offset just add '-' before the offset
    private Vector3 CalculateSensorPosition(float forwardOffset, float rightOffset)
    {
        Vector3 startingPosition = transform.position;

        startingPosition.y += _verticalOffset;
        startingPosition += transform.forward * forwardOffset;
        startingPosition += transform.right * rightOffset;

        return startingPosition;
    }

    private Vector3 CalculateSensorPosition(Vector3 sensorParameters)
    {
        Vector3 startingPosition = transform.position;

        startingPosition.y += _verticalOffset;
        startingPosition += transform.forward * sensorParameters.x;
        startingPosition += transform.right * sensorParameters.y;

        return startingPosition;
    }

    public List<(bool, float)> CollectSensorOutputs()
    {
        List<(bool, float)> sensorOutputs = new();

        RaycastHit hitInformation; // ray hit information
        Vector3 sensorStartingPosition;

        foreach(var sensor in _sensors)
        {
            sensorStartingPosition = CalculateSensorPosition(sensor);

            var hasHit = Physics.Raycast(sensorStartingPosition, Quaternion.AngleAxis(sensor.z, transform.up) * transform.forward, out hitInformation, _sensorLength, _layerMask);

            var hasCollidedWithAWall = (hasHit) ? (hitInformation.distance < _hitThreshold) : false;
            var distanceFromAWall = (hasHit) ? hitInformation.distance : _sensorLength;

            sensorOutputs.Add((hasCollidedWithAWall, distanceFromAWall));
        }

        return sensorOutputs;
    }
}
