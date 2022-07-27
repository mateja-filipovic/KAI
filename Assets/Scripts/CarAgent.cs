using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CarAgent : Agent
{

    private CarController _carController;
    public CheckpointManager _checkpointManager;

    public override void Initialize()
    {
        _carController = GetComponent<CarController>();
    }

    public override void OnEpisodeBegin()
    {
        //_checkpointManager.ResetCheckpoints();
        //_carController.Respawn();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float steer = actions.ContinuousActions[0];
        float accel = actions.ContinuousActions[1];
        _carController.Re(accel, steer);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        //sensor.AddObservation(diff / 20f);
        //AddReward(-0.001f);
        //AddVectorObs();
    }

    // manual testinf
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.ContinuousActions;
        actions[0] = Input.GetAxis("Horizontal"); // steering
        actions[1] = Input.GetAxis("Vertical"); // acceleration
    }
}
