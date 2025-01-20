using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using System.Collections;

public class GameplayHUD : Sounds
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _pauseCanvas;
    [SerializeField] private Canvas _loseCanvas;
    [SerializeField] private GameObject _mobileCanvas;
    [SerializeField] private Button _adRestartButton;
    [SerializeField] private Points _points;
    [SerializeField] private Text _currentCount;
    [SerializeField] private Text _bestCount;
    [SerializeField] private GameObject _bgAudio;
    [SerializeField] private GameObject _objectToDeactivate; // ������, ������� ����� ��������������

    [SerializeField] private string rewardID; // ID ��� reward �������
    private Transform _lastCheckpoint;
    private int _pointsCount;
    private bool _isMobile;
    private bool _canTap;
    private bool _isAdPlaying;
    private bool _rewardClaimed = false; // ���� ��� ������������ ��������� ������
    private int _loseCount = 0; // ������� ������� ������ Lose

    // Array of AudioSource components associated with each sound
    [SerializeField] private AudioSource[] audioSources;

    private void Awake()
    {
        _canTap = true;
    }

    private void Start() {
        Pause();
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    public void Pause()
    {
        PlaySound(sounds[2]);
        Time.timeScale = 0;
        _mainCanvas.gameObject.SetActive(false);
        _pauseCanvas.gameObject.SetActive(true);
        if (_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(false);
        }
    }

    public void Continue()
    {
        if (_canTap)
        {
            Time.timeScale = 1;
            _mainCanvas.gameObject.SetActive(true);
            _pauseCanvas.gameObject.SetActive(false);
            if (_isMobile)
            {
                _mobileCanvas.gameObject.SetActive(true);
            }
            _points.StartCount();
        }
    }

    public void Restart()
    {
        AudioListener.volume = 1;
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = false;
        }

        if (_canTap)
        {
            PlaySound(sounds[0]);
            Time.timeScale = 1;
            int best = PlayerPrefs.GetInt("Best");
            _pointsCount = _points.PointsCount;
            if (_pointsCount > best)
            {
                PlayerPrefs.SetInt("Best", _pointsCount);
            }

            SceneManager.LoadScene("Gameplay");
        }
    }

    public void ToMenu()
    {
        if (_canTap)
        {
            Time.timeScale = 1;
            PlaySound(sounds[1]);
            int best = PlayerPrefs.GetInt("Best");
            _pointsCount = _points.PointsCount;
            if (_pointsCount > best)
            {
                PlayerPrefs.SetInt("Best", _pointsCount);
            }

            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Lose(Transform lastCheckpoint)
    {
        _loseCount++; // ����������� ������� ������� ������ Lose

        int best = PlayerPrefs.GetInt("Best");
        _pointsCount = _points.PointsCount;
        _currentCount.text = _pointsCount.ToString();
        if (_pointsCount > best)
        {
            _bestCount.text = _pointsCount.ToString();
            PlayerPrefs.SetInt("Best", _pointsCount);
        }
        else
        {
            _bestCount.text = best.ToString();
        }

        Time.timeScale = 0;

        YandexGame.NewLeaderboardScores("Point", Points.liderboardPoints);
        _loseCanvas.gameObject.SetActive(true);
        _mainCanvas.gameObject.SetActive(false);
        _lastCheckpoint = lastCheckpoint;
        if (_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(false);
        }

        // ���������, �������� �� �������� �������� 2
        if (_loseCount >= 2 && _objectToDeactivate != null)
        {
            _objectToDeactivate.SetActive(false); // ������������ ������
        }
    }

    public void ContinueAd()
    {
        if (_canTap)
        {
            _bgAudio.SetActive(false);
            _canTap = false;
            if (Application.isEditor)
            {
                Rebirth();
                SetTime();
            }
            else
            {
                // ������ �� ������ ��������
                Rebirth();
            }
        }
    }
    public GameObject rewardbtn;
    public void MyRewardAdvShow(int id)
    {
        AudioListener.volume = 0;
        _isAdPlaying = true;
        YandexGame.RewVideoShow(id);
        ContBTN.SetActive(true);
        rewardbtn.SetActive(false);
    }

    public void Rebirth()
    {
        _loseCanvas.gameObject.SetActive(false);
        _adRestartButton.gameObject.SetActive(false);
        _mainCanvas.gameObject.SetActive(true);
        _levelManager.LastChance(_lastCheckpoint);
        if (_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(true);
        }
        _points.StartCount();
    }

    public void SetTime()
    {
        _bgAudio.SetActive(true);
        _canTap = true;
    }

    private void SetPlatform(bool isMobile)
    {
        _isMobile = isMobile;
        if (!_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(false);
        }
    }

    public void GoCont() {
        StartCoroutine(DelayedButtonRemoval());

        AudioListener.volume = 1;
        _mainCanvas.gameObject.SetActive(true);
        _pauseCanvas.gameObject.SetActive(false);
        if (_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(true);
        }
        _points.StartCount();
        _loseCanvas.gameObject.SetActive(false);
        _adRestartButton.gameObject.SetActive(false);
        _mainCanvas.gameObject.SetActive(true);
        if(_lastCheckpoint != null){
            _levelManager.LastChance(_lastCheckpoint);
        }
    }

    private IEnumerator DelayedButtonRemoval()
    {
        yield return new WaitForSeconds(0.1f);
        if (ContBTN != null) {
            ContBTN.GetComponent<Button>().enabled = false;
            ContBTN.GetComponent<Image>().enabled = false;
        }
    }

    public GameObject ContBTN;

    private void Rewarded(int id)
    {
        if (id == 1 && !_rewardClaimed) {
            _rewardClaimed = true;
            ContBTN.SetActive(true);
        }
        else
        {
            if(ContBTN != null) ContBTN.SetActive(false);
        }
    }
}
