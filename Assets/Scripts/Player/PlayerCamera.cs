using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _boat;
    private Transform _camera;

    private void Awake()
    {
        _camera = transform;
    }

    private void FixedUpdate()
    {
        _camera.position = Vector3.Lerp(_camera.position, _boat.TransformPoint(_offset), _moveSpeed * Time.fixedDeltaTime);

        Vector3 rotationDirection = _boat.position - _camera.position;
        Quaternion newRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);

        _camera.rotation = Quaternion.Lerp(_camera.rotation, newRotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}
