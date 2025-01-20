using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        _text.text = PlayerPrefs.GetInt("Best").ToString();
    }
}
