using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Intrerfaces
{
    public class LOADER : MonoBehaviour
    {
       
            void Start()
            {
                Debug.Log("Loading Main scene...");
                SceneManager.LoadScene("MainScene");
            }
        }
    }
