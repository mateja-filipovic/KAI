using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public delegate void OnCorrectCheckpointPassedAction();
    public static event OnCorrectCheckpointPassedAction OnCorrectCheckpointPassed;


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
            checkpoint.CheckpointManager = this;
        }

        foreach(Transform car in _cars)
            _nextCheckpointIndexes.Add(0);
    }

    public void ResetCheckpointsForCar(Transform car)
    {
        var carIndex = _cars.IndexOf(car);
        _nextCheckpointIndexes[carIndex] = 0;
    }

    public Vector3 GetNextCheckpointPosition(Transform car) =>
        _checkpoints[_nextCheckpointIndexes[_cars.IndexOf(car)]].transform.position;

    public void OnCheckpointReached(Checkpoint checkpoint, Transform car)
    {
        var passedCheckpointIndex = _checkpoints.IndexOf(checkpoint);
        var carIndex = _cars.IndexOf(car);
        var correctCheckpointIndex = _nextCheckpointIndexes[carIndex];

        if(passedCheckpointIndex != correctCheckpointIndex)
            return;

        // notify subscribers that the correct checkpoint was passed
        if(OnCorrectCheckpointPassed is not null)
            OnCorrectCheckpointPassed();

        SetNextCheckpointForCar(carIndex);
    }

    private void SetNextCheckpointForCar(int carIndex) =>
        _nextCheckpointIndexes[carIndex] = (_nextCheckpointIndexes[carIndex] + 1) % _checkpointCount;
}
