using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CarAgent : Agent
{

    [SerializeField]
    private float _hitPenalty;

    [SerializeField]
    private float _reachCheckpointReward;

    [SerializeField]
    private float _movingTowardsCheckpointReward;

    [SerializeField]
    private float _speedReward;

    public float HitPenalty { get => _hitPenalty; set => _hitPenalty = value; }
    public float ReachCheckpointReward { get => _reachCheckpointReward; set => _reachCheckpointReward = value; }
    public float MovingTowardsCheckpointReward { get => _movingTowardsCheckpointReward; set => _movingTowardsCheckpointReward = value; }
    public float SpeedReward { get => _speedReward; set => _speedReward = value; }


    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";


    private CarController _carController;
    
    private CheckpointManager _checkpointManager;


    private bool _isAccelerating;
    private bool _endCurrentEpisode;
    private float _sensorsPenaltyTotal; // a penalty is added for every collision detected by the sensor system

    public override void Initialize()
    {
        _carController = GetComponent<CarController>();
        _checkpointManager = GameObject.Find("Checkpoints").GetComponent<CheckpointManager>();

        ValidateGameObjectInitialization();
        
        CheckpointManager.OnCorrectCheckpointPassed += OnCorrectCheckpointPassedEventHandler;
    }

    public void OnCorrectCheckpointPassedEventHandler() =>
        AddReward(_reachCheckpointReward);

    public override void OnEpisodeBegin()
    {
        _endCurrentEpisode = false;
        _isAccelerating = false;
        _sensorsPenaltyTotal = 0;

        _checkpointManager.ResetCheckpointsForCar(transform);
        _carController.Respawn();
    }

    public void Update()
    {
        if (!_endCurrentEpisode)
            return;

        // There was a collision. Collect sensors penalty and start over.
        AddReward(_sensorsPenaltyTotal);
        EndEpisode();
        OnEpisodeBegin();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float steer = actions.ContinuousActions[0];
        float accel = actions.ContinuousActions[1];
        _carController.AcceptInput(accel, steer);

        CalculateRewardsOnActionsReceived();
    }

    private void CalculateRewardsOnActionsReceived()
    {
        var directionReward = Vector3.Dot(_carController.Car.velocity.normalized, GetCarDirectionInReferenceToNextCheckpoint()) * _movingTowardsCheckpointReward;
        var accelerationReward = _carController.Car.velocity.normalized.magnitude * _speedReward;

        AddReward(directionReward);
        AddReward(accelerationReward);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // The speed of the car
        sensor.AddObservation(_carController.Car.velocity.normalized.magnitude);

        // Sensor outputs
        InterpretateSensorOutputs(sensor);

        // Direction and distance to the next checkpoint
        var direction = GetCarDirectionInReferenceToNextCheckpoint();
        float a = Vector3.Dot(_carController.Car.velocity.normalized, direction);
        sensor.AddObservation(a);

        // Distance to the next checkpoint, normalized
        Vector3 difference = _checkpointManager.GetNextCheckpointPosition(this.transform) - this.transform.position;
        sensor.AddObservation(difference.normalized);

        // Is the car currently accelerating
        sensor.AddObservation(_isAccelerating);
    }

    private Vector3 GetCarDirectionInReferenceToNextCheckpoint() =>
        (_checkpointManager.GetNextCheckpointPosition(this.transform) - this.transform.position).normalized;

    private void InterpretateSensorOutputs(VectorSensor sensor)
    {
        List<(bool, float)> sensorOutputs = _carController.GetSensorOutput();

        foreach(var output in sensorOutputs)
            sensor.AddObservation(output.Item2);
    }

    void OnCollisionEnter(Collision collision)
    {
        _endCurrentEpisode = true;
        _sensorsPenaltyTotal += HitPenalty;
    }

    // manual testing
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.ContinuousActions;
        actions[0] = Input.GetAxis(AXIS_HORIZONTAL); // steering
        actions[1] = Input.GetAxisRaw(AXIS_VERTICAL); // acceleration
        _isAccelerating = actions[1] > 0;
    }

    private void ValidateGameObjectInitialization()
    {
        if(_carController is null)
            Debug.LogError("The _carController object is null");

        if(_checkpointManager is null)
            Debug.LogError("The _checkpointManager object is null");
    }
}
