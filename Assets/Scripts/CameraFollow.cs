using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Property fields
    [SerializeField] 
    private Vector3 _offset;

    [SerializeField] 
    private Transform _target;

    [SerializeField] 
    private float _translateSpeed;

    [SerializeField] 
    private float _rotationSpeed;
    #endregion

    public Vector3 Offset { get => _offset; set => _offset = value; }
    public Transform Target { get => _target; set => _target = value; }
    public float TranslateSpeed { get => _translateSpeed; set => _translateSpeed = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }
   
    private void HandleTranslation()
    {
        var targetPosition = _target.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _translateSpeed * Time.deltaTime);
    }
    
    private void HandleRotation()
    {
        var direction = _target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}