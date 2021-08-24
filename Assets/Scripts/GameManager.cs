using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    public int goldAmount;

    [Header("Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject exitAskPanel;
    [SerializeField] GameObject gameOverPanel;

    private void Awake()
    {
        int curInfo = PlayerPrefs.GetInt("Re");

        Screen.SetResolution(1920, 1080, false);
        mainMenuPanel.SetActive(curInfo == 0 ? true : false);
        if(curInfo == 0)
        {
            mainMenuPanel.SetActive(true);
        }
        else
        {
            mainMenuPanel.SetActive(false);
            StartGame();
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }
    IEnumerator StartGameCor()
    {
        stageManager.fadeAni.SetBool("Black", true);

        
        yield return new WaitForSeconds(1);
        stageManager.fadeAni.SetBool("Black", false);

        mainMenuPanel.SetActive(false);
        stageManager.PickMap();
    }

    public void ExitAsk(bool active)
    {
        exitAskPanel.SetActive(active);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Re", 1);
        SceneManager.LoadScene("InGame");
    }

    public void MainMenu()
    {
        PlayerPrefs.SetInt("Re", 0);
        SceneManager.LoadScene("InGame");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Re", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) RestartGame();
    }
}
