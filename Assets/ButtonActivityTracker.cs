using UnityEngine;
using UnityEngine.UI;

public class ButtonActivityTracker : MonoBehaviour
{
    public Button targetButton;
    public GameObject BTN;
    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
        }
    }

    void OnButtonClick()
    {
        targetButton.interactable = false;
        BTN.SetActive(false);
    }
}
