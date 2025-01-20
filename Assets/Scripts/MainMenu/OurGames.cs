using System.Runtime.InteropServices;
using UnityEngine;

public class OurGames : Sounds
{
 
    [SerializeField] private Links[] _links;
    [SerializeField] private Canvas _ourGamesCanvas;
    [SerializeField] private Canvas _mainCanvas;

    private bool _isRu;

    private void Awake()
    {
        if (!Application.isEditor)
        {
     
        }
    }

    public void Open()
    {
        PlaySound(sounds[0], 1f, false, 1.2f, 1.4f);
        if (Time.timeScale == 1)
        {
            _ourGamesCanvas.gameObject.SetActive(true);
            _mainCanvas.gameObject.SetActive(false);
            
        }
    }   

    public void Close()
    {
        PlaySound(sounds[0], 1f, false, 0.92f, 0.97f);
        _ourGamesCanvas.gameObject.SetActive(false);
        _mainCanvas.gameObject.SetActive(true);
    }

    public void StartGame(int count)
    {
        if (_isRu)
        {
            Application.OpenURL(_links[count]._ruLink);
        }
        else
        {
            Application.OpenURL(_links[count]._enLink);
        }
    }
}