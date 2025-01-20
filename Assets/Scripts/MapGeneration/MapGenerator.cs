using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Segment[] _segments;
    [SerializeField] private int _maxGeneratedSegments;

    private List<Segment> _generatedSegments = new List<Segment>();
    private int _lastGeneratedSegmentIndex;
    private List<int> _generateSeed = new List<int>();
    private int _currentSeedCount;

    #region MONO

    private void OnEnable()
    {
        GenerateZone.OnGenerateZoneReached += Generate;
    }

    private void OnDisable()
    {
        GenerateZone.OnGenerateZoneReached -= Generate;
    }

    #endregion

    private void Awake()
    {
        _generateSeed = GenerateSeed();
        Generate(Vector3.zero);
    }

    private List<int> GenerateSeed()
    {
        List<int> seed = new List<int>();
        int a;

        for (int i = 0; i < _segments.Length; i++)
        {
            a = Random.Range(0, _segments.Length);
            while (seed.Contains(a))
            {
                a = Random.Range(0, _segments.Length);
            }
            seed.Add(a);
        }
        return seed;
    }

    private void Generate(Vector3 endPosition)
    {
        int newSegmentIndex;

        if (_currentSeedCount > _generateSeed.Count - 1)
        {
            _currentSeedCount = 0;
            GenerateSeed();
            newSegmentIndex = _generateSeed[_currentSeedCount];
        }
        else
        {
            newSegmentIndex = _generateSeed[_currentSeedCount];
            _currentSeedCount++;
        }

        Segment segment = Instantiate(_segments[newSegmentIndex]);
        segment.transform.position = endPosition - segment.StartPivot.position;

        _lastGeneratedSegmentIndex = newSegmentIndex;

        _generatedSegments.Add(segment);
        if(_generatedSegments.Count >= _maxGeneratedSegments)
        {
            Destroy(_generatedSegments[0].gameObject);
            _generatedSegments.RemoveAt(0);
        }
    }
}