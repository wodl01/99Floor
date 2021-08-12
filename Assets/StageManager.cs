using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] PoolManager pool;
    [SerializeField] PlayerState playerState;
    [SerializeField] DoorScript door;

    [Header("Stage")]
    [SerializeField] List<GameObject> firstMaps;
    [SerializeField] GameObject curMap;

    public int curStage;
    public int maxStage;

    public int enemyAmount;

    public bool allKill;

    [Header("Ui")]
    public Text curStageText;
    public Text leftEnemyText;
    public Text boxCheckText;

    [Header("Animation")]
    [SerializeField] Animator stageClearAni;
    [SerializeField] Animator fadeAni;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) PickMap();
    }

    public void PickMap()
    {
        allKill = false;
        door.DoorOpen(false);

        curMap.SetActive(false);

        
        if(curStage < 30)
        {
            int randomCode = Random.Range(0, firstMaps.Count);
            firstMaps[randomCode].SetActive(true);
            curMap = firstMaps[randomCode];
            curMap.transform.GetChild(0).GetComponent<BoxPlaceManager>().PlaceObject();
            firstMaps.RemoveAt(randomCode);

            playerState.player.transform.position = curMap.transform.GetChild(1).transform.position;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        EnemySpawnManager enemySpawn = curMap.transform.GetChild(0).GetComponent<EnemySpawnManager>();
        curStage++;
        curStageText.text = "-" + curStage.ToString() + "층-";
        for (int i = 0; i < enemySpawn.enemySpawnPoint.Length; i++)
        {
            int curSpawnCode = enemySpawn.enemySpawnPoint[i].monsterCode;
            if (curSpawnCode == 0) return;
            pool.EnemyInstantiate("Enemy" + curSpawnCode, enemySpawn.enemySpawnPoint[i].transform.position);
            enemyAmount++;
        }
        leftEnemyText.text = "남은 적:" + enemyAmount.ToString();
    }

    public IEnumerator WarningText(int warningCode)
    {
        switch (warningCode)
        {
            case 0:
                boxCheckText.text = "적을 모두 무찌르십시오.";
                break;
            case 1:
                boxCheckText.text = "스테이지를 클리어 해주십시오.";
                break;
        }
        boxCheckText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        boxCheckText.gameObject.SetActive(false);
    }

    public void ClearCheck()
    {
        leftEnemyText.text = "남은 적:" + enemyAmount.ToString();
        if (enemyAmount == 0)
        {
            StageClear();
        }
    }

    public void StageClear()
    {
        stageClearAni.SetTrigger("Start");
        door.DoorOpen(true);
        allKill = true;
    }

    public IEnumerator NextMapLoad()
    {
        fadeAni.SetBool("Black", true);
        yield return new WaitForSeconds(1);
        curMap.transform.GetChild(0).GetComponent<BoxPlaceManager>().ClearBox();
        PickMap();
        fadeAni.SetBool("Black", false);
    }
}
