using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] PlayerState playerState;

    [Header("Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject exitAskPanel;
    [SerializeField] GameObject gameOverPanel;

    // 0 매인메뉴
    // 1 인게임

    private void Start()
    {
        int curInfo = PlayerPrefs.GetInt("Re");

        playerState.gold = PlayerPrefs.GetInt("Gold");
        playerState.key = PlayerPrefs.GetInt("Key");
        playerState.bomb = PlayerPrefs.GetInt("Bomb");

        Screen.SetResolution(1920, 1080, false);
        mainMenuPanel.SetActive(curInfo == 0 ? true : false);
        if (curInfo == 0)
        {
            mainMenuPanel.SetActive(true);
            SoundManager.PlayBGM("MainMenu");
        }
        else
        {
            mainMenuPanel.SetActive(false);
            StartGame();
            SoundManager.PlayBGM("WaitingRoom");
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
        SoundManager.PlayBGM("WaitingRoom");
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
        SaveInfos();
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Re", 1);
        SaveInfos();
        SceneManager.LoadScene("InGame");
    }

    public void MainMenu()
    {
        PlayerPrefs.SetInt("Re", 0);
        SaveInfos();
        SceneManager.LoadScene("InGame");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Re", 0);
        SaveInfos();
    }

    void SaveInfos()
    {
        PlayerPrefs.SetInt("Gold", playerState.gold / 2);
        PlayerPrefs.SetInt("Key", playerState.key);
        PlayerPrefs.SetInt("Bomb", playerState.bomb);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) RestartGame();
    }
}
