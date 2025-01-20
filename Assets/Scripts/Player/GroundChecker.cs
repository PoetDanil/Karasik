using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _checkRadius;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public bool OnGround()
    {
        if(Physics.CheckSphere(_transform.position, _checkRadius, _layerMask))
        {
            return true;
        }
        return false;
    }
}