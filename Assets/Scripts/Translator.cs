using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{


    [TextArea(2,3)][SerializeField] private string _engText;

    private void Awake()
    {
        if (!Application.isEditor)
        {
       
        }
    }
}
