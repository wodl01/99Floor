using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [Header("Manager")]
    public static StageManager stageManager;
    [SerializeField] PoolManager pool;
    [SerializeField] PlayerState playerState;
    [SerializeField] DoorScript door;

    [Header("Stage")]
    [SerializeField] List<GameObject> firstMaps;
    [SerializeField] List<GameObject> secondMaps;
    [SerializeField] GameObject curMap;

    public int curStage;
    public int maxStage;

    public int enemyAmount;

    public bool allKill;

    [Header("Ui")]
    public Text curStageText;
    public Text leftEnemyText;

    [Header("Animation")]
    [SerializeField] Animator stageClearAni;
    [SerializeField] Animator fadeAni;

    [Header("Pools")]
    public List<GameObject> poolDestroyList;
    public List<GameObject> destroyList;

    private void Awake() => stageManager = this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) PickMap();
    }

    public void PickMap()
    {
        allKill = false;
        door.DoorOpen(false);

        curMap.SetActive(false);

        
        if(curStage <= 30)
        {
            int randomCode = Random.Range(0, firstMaps.Count);
            firstMaps[randomCode].SetActive(true);
            curMap = firstMaps[randomCode];
            curMap.transform.GetChild(0).GetComponent<BoxPlaceManager>().PlaceObject();
            firstMaps.RemoveAt(randomCode);

            playerState.player.transform.position = curMap.transform.GetChild(1).transform.position;
            SpawnEnemy();
        }
        else if(curStage > 30 && curStage <= 60)
        {
            int randomCode = Random.Range(0, secondMaps.Count);
            secondMaps[randomCode].SetActive(true);
            curMap = secondMaps[randomCode];
            curMap.transform.GetChild(0).GetComponent<BoxPlaceManager>().PlaceObject();
            secondMaps.RemoveAt(randomCode);

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
        ObjectsDestroy();
        fadeAni.SetBool("Black", false);
    }

    public void ObjectsDestroy()
    {
        for (int i = 0; i < poolDestroyList.Count; i++)
        {
            poolDestroyList[i].SetActive(false);
        }
        for (int i = 0; i < destroyList.Count; i++)
        {
            Destroy(destroyList[i]);
        }
    }
}
