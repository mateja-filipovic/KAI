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

    // keeping track of the next checkpoints for each car
    private List<int> _nextCheckpointIndexes;

    private List<Checkpoint> _checkpoints = null!;

    private int _checkpointCount = 0;

    private void Awake()
    {
        _checkpoints = new();
        _nextCheckpointIndexes = new();

        // cycle through all the children
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

    // public float MaxTimeToReachNextCheckpoint = 30f;
    // public float TimeLeft = 30f;
    
    // public CarAgent kartAgent;
    // public Checkpoint nextCheckPointToReach;
    
    // private int CurrentCheckpointIndex;
    // private List<Checkpoint> Checkpoints;
    // private Checkpoint lastCheckpoint;

    // public event Action<Checkpoint> reachedCheckpoint; 

    // void Awake()
    // {
    //     Checkpoints = new List<Checkpoint>(GetComponentsInChildren<Checkpoint>());
    // }

    // void Start()
    // {
    //     ResetCheckpoints();
    // }

    // public void ResetCheckpoints()
    // {
    //     CurrentCheckpointIndex = 0;
    //     TimeLeft = MaxTimeToReachNextCheckpoint;
        
    //     SetNextCheckpoint();
    // }

    // private void Update()
    // {
    //     TimeLeft -= Time.deltaTime;

    //     if (TimeLeft < 0f)
    //     {
    //         kartAgent.AddReward(-1f);
    //         kartAgent.EndEpisode();
    //     }
    // }

    // public void CheckPointReached(Checkpoint checkpoint)
    // {
    //     if (nextCheckPointToReach != checkpoint) return;
        
    //     lastCheckpoint = Checkpoints[CurrentCheckpointIndex];
    //     reachedCheckpoint?.Invoke(checkpoint);
    //     CurrentCheckpointIndex++;

    //     if (CurrentCheckpointIndex >= Checkpoints.Count)
    //     {
    //         kartAgent.AddReward(0.5f);
    //         kartAgent.EndEpisode();
    //     }
    //     else
    //     {
    //         kartAgent.AddReward((0.5f) / Checkpoints.Count);
    //         SetNextCheckpoint();
    //     }
    // }

    // private void SetNextCheckpoint()
    // {
    //     if (Checkpoints.Count > 0)
    //     {
    //         TimeLeft = MaxTimeToReachNextCheckpoint;
    //         nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];
            
    //     }
    // }
}
