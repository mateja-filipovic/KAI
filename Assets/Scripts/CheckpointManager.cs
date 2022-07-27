using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    #region Property Fields

    [SerializeField]
    private Transform _checkpointsTransform;

    [SerializeField]
    private List<Transform> _cars;

    #endregion
    
    // Parent containing all the checkpoints
    public Transform CheckpointsTransform { get => _checkpointsTransform; set => _checkpointsTransform = value; }

    // List of all the cars
    public List<Transform> Cars { get => _cars; set => _cars = value; }



    // Keeping track of the next checkpoints for each car
    private List<int> _nextCheckpointIndexes;

    // List of all available checkpoints
    private List<Checkpoint> _checkpoints = null!;

    private int _checkpointCount = 0;

    private void Awake()
    {
        _checkpoints = new();
        _nextCheckpointIndexes = new();

        InitializeCheckpoints();
    }

    private void InitializeCheckpoints()
    {
        foreach(Transform cpTransform in _checkpointsTransform)
        {
            var checkpoint = cpTransform.GetComponent<Checkpoint>();
            _checkpoints.Add(checkpoint);
            _checkpointCount++;
            _nextCheckpointIndexes.Add(0);
            checkpoint.CheckpointManager = this;
        }       
    }

    public void OnCheckpointReached(Checkpoint checkpoint, Transform car)
    {
        var passedCheckpointIndex = _checkpoints.IndexOf(checkpoint);
        var carIndex = _cars.IndexOf(car);
        var correctCheckpointIndex = _nextCheckpointIndexes[carIndex];

        if(passedCheckpointIndex != correctCheckpointIndex)
            return;

        Debug.Log($"Car No. {carIndex} passed through checkpoint No. {_nextCheckpointIndexes[carIndex]}");

        SetNextCheckpointForCar(carIndex);
    }

    private void SetNextCheckpointForCar(int carIndex) =>
        _nextCheckpointIndexes[carIndex] = (_nextCheckpointIndexes[carIndex] + 1) % _checkpointCount;
}
