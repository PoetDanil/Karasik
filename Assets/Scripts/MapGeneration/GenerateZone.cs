using System;
using UnityEngine;

public class GenerateZone : MonoBehaviour
{
    public static Action<Vector3> OnGenerateZoneReached;

    [SerializeField] private Segment _segment;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Boat>() != null)
        {
            OnGenerateZoneReached?.Invoke(_segment.EndPivot.position);
            gameObject.SetActive(false);
        }
    }
}