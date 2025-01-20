using System.Collections;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float _maxRotateAngle;
    [SerializeField] private float _addSpeed;
    [SerializeField] private float _addTorque;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpForceHeight;
    [SerializeField] private float _jumpForceWeight;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Transform _head;
    [SerializeField] private GameObject _particles;

    private Animator _anim;
    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private Transform _transform;

    private float _addingSpeedInTime = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _transform = transform;
        _anim = GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(AddSpeedInTime());
    }

    private void OnEnable()
    {
        PlayerInput.OnJump += Jump;
    }

    private void OnDisable()
    {
        PlayerInput.OnJump -= Jump;
    }

    private void FixedUpdate()
    {
        _rb.velocity += _transform.right * _playerInput.MoveInput * _addSpeed * _addingSpeedInTime * Time.fixedDeltaTime;
        _anim.SetFloat("IsRide", _playerInput.MoveInput);

        _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(0,_maxRotateAngle * _playerInput.RotationInput, 0), _addTorque * Time.fixedDeltaTime);
        _head.localRotation = Quaternion.Lerp(_head.localRotation, Quaternion.Euler(0, -_maxRotateAngle * _playerInput.RotationInput, 0), _addTorque * Time.fixedDeltaTime);

        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);

        if(_rb.velocity.magnitude >= _maxSpeed - 1)
        {
            _particles.SetActive(true);
        }
        else
        {
            _particles.SetActive(false);
        }
    }

    private void Jump()
    {
        if (_groundChecker.OnGround())
        {
            _rb.AddForce((transform.right * _jumpForceWeight + Vector3.up * _jumpForceHeight));
        }
    }

    private IEnumerator AddSpeedInTime() {
        while (_addingSpeedInTime < 2.5f) {
            _addingSpeedInTime += 0.1f;
            yield return new WaitForSeconds(10);
            Debug.Log(_addingSpeedInTime);
        }
    } 
}