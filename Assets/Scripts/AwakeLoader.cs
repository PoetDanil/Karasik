using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FNAF
{
    public class AwakeLoader : MonoBehaviour
    {
   

        private void Start()
        {

        }

        public void Loading()
        {
            SceneManager.LoadScene(1);
        }
    }
}