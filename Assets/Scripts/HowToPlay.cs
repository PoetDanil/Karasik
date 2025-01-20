using UnityEngine;
using YG;
public class HowToPlay : MonoBehaviour
{
    [SerializeField] private GameObject _mobile;
    [SerializeField] private GameObject _pc;

    private void Show()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            _mobile.SetActive(true);
            _pc.SetActive(false);
        }
        else
        {
            _mobile.SetActive(false);
            _pc.SetActive(true);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            gameObject.SetActive(false);
        }
    }
}
