using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int goldAmount;

    [SerializeField] GameObject exitAskPanel;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
       // DontDestroyOnLoad(gameObject);
    }

    public void ExitAsk(bool active)
    {
        exitAskPanel.SetActive(active);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
