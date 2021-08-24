using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHi : MonoBehaviour
{
    public static SceneManagerHi sceneManager;


    public void Awake() => sceneManager = this;

    public void SceneChange(int index)
    {
        switch (index)
        {
            case 0:
                SceneManager.LoadScene("Main");
                break;
            case 1:
                SceneManager.LoadScene("InGame");
                break;
        }
    }

    
}
