using System.Collections;
using UnityEngine;

public class CheckDeath : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private float _checkDelay;
    [SerializeField] private float _deathYPosition;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        StartCoroutine(IsDeath());
    }

    private IEnumerator IsDeath()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(_checkDelay);
            if(_transform.position.y <= _deathYPosition)
            {
                _levelManager.Respawn();
                Debug.Log("You are dead. I'm sorry :) (You can include here something, just delete this string)");
            }
        }
    }
}