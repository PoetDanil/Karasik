using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;
public class Points : MonoBehaviour
{
    public int PointsCount { private set; get; }

    [SerializeField] private float _countDelay;
    [SerializeField] private int _multiplier = 10;
    [SerializeField] private Transform _player;

    private Text _pointText;
    private float _lastPos;

    // ��� ����������
    private float timer = 0f;
    public float interval = 3f;
    public static int liderboardPoints;
    
    public void StartCount()
    {
        StartCoroutine(Counter());
    }
    public void UpdateLByg() {

        //YandexGame.NewLeaderboardScores("Point", PointsCount);
    }
    
    private void Start()
    {
        _pointText = GetComponent<Text>();
        _lastPos = _player.position.x;
        StartCount();
    }
    private void Update()
    {
        liderboardPoints = PointsCount;
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            UpdateLByg();
            timer = 0f; // ����� �������
        }
    }
    private IEnumerator Counter()
    {
        while (true)
        {
            yield return new WaitForSeconds(_countDelay);
            if(_player.position.x > _lastPos)
            {
                PointsCount += (int)((_player.position.x - _lastPos) * _multiplier);
                _lastPos = _player.position.x;   
                _pointText.text = PointsCount.ToString();
            }
        }
    }
}
