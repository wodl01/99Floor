using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    public int goldAmount;

    [SerializeField] GameObject mainMenuPanel;

    [SerializeField] GameObject exitAskPanel;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
        mainMenuPanel.SetActive(true);
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
}
