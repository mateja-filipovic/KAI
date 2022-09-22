using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Property fields
    [SerializeField] 
    private Vector3 _offset;

    [SerializeField] 
    private List<Transform> _targets;

    [SerializeField] 
    private float _translateSpeed;

    [SerializeField] 
    private float _rotationSpeed;
    #endregion

    public Vector3 Offset { get => _offset; set => _offset = value; }
    public List<Transform> Targets { get => _targets; set => _targets = value; }
    public float TranslateSpeed { get => _translateSpeed; set => _translateSpeed = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

    private int _currentTargetIndex = 0;

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }
   
    private void HandleTranslation()
    {
        var targetPosition = _targets[_currentTargetIndex].TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _translateSpeed * Time.deltaTime);
    }
    
    private void HandleRotation()
    {
        var direction = _targets[_currentTargetIndex].position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _currentTargetIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            _currentTargetIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            _currentTargetIndex = 2;
    }
}