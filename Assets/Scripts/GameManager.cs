using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] PlayerState playerState;
    public int goldAmount;

    [Header("Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject exitAskPanel;
    [SerializeField] GameObject gameOverPanel;

    private void Awake()
    {
        int curInfo = PlayerPrefs.GetInt("Re");
        int GoldAmount = PlayerPrefs.GetInt("Gold");

        goldAmount = GoldAmount;
        playerState.gold = GoldAmount;

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
        goldAmount = playerState.gold / 2;
        PlayerPrefs.SetInt("Gold", goldAmount);
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
        PlayerPrefs.SetInt("Gold", goldAmount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) RestartGame();
    }
}
