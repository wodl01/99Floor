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
    [SerializeField] GameObject waitRoom;
    [SerializeField] List<GameObject> firstMaps;
    [SerializeField] List<GameObject> secondMaps;
    [SerializeField] GameObject bossRoom1;
    public GameObject curMap;
    public float monsterStageStrong;

    public int curStage;
    public int maxStage;

    public int enemyAmount;
    public List<GameObject> enemyList;

    public bool allKill;

    [Header("Ui")]
    public Text curStageText;
    public Text leftEnemyText;

    [Header("Animation")]
    [SerializeField] Animator stageClearAni;
    public Animator fadeAni;

    private void Awake() => stageManager = this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) PickMap();
    }

    public void PickMap()
    {
        allKill = false;
        door.DoorOpen(false);

        if(curMap != null)
        curMap.SetActive(false);
        waitRoom.SetActive(false);

        playerState.player.GetComponent<PlayerControlScript>().hitboxScript.PlayerHit(0);

        if (curStage == 0)
        {
            curMap = waitRoom;
            waitRoom.SetActive(true);
            curMap.transform.GetChild(0).GetComponent<BoxPlaceManager>().PlaceObject();
            playerState.player.transform.position = curMap.transform.GetChild(1).transform.position;

            door.DoorOpen(true);
            allKill = true;
        } 
        else if(curStage <= 30)
        {
            int randomCode = Random.Range(0, firstMaps.Count);
            firstMaps[randomCode].SetActive(true);
            curMap = firstMaps[randomCode];
            curMap.transform.GetChild(0).GetComponent<BoxPlaceManager>().PlaceObject();
            firstMaps.RemoveAt(randomCode);

            playerState.player.transform.position = curMap.transform.GetChild(1).transform.position;
            SpawnEnemy();

            SoundManager.PlayBGM("FirstMap");
        }
        else if(curStage > 31 && curStage <= 60)
        {
            int randomCode = Random.Range(0, secondMaps.Count);
            secondMaps[randomCode].SetActive(true);
            curMap = secondMaps[randomCode];
            curMap.transform.GetChild(0).GetComponent<BoxPlaceManager>().PlaceObject();
            secondMaps.RemoveAt(randomCode);

            playerState.player.transform.position = curMap.transform.GetChild(1).transform.position;
            SpawnEnemy();

            SoundManager.PlayBGM("SecondMap");
        }
        else if(curStage == 31)
        {
            bossRoom1.SetActive(true);
            curMap = bossRoom1;
            curMap.transform.GetChild(0).GetComponent<BoxPlaceManager>().PlaceObject();
            playerState.player.transform.position = curMap.transform.GetChild(1).transform.position;

            SoundManager.PlayBGM("BossMap");
        }
        else if(curStage == 61)
        {

        }
        door.ChangeTransform();
        curStage++;
        curStageText.text = "-" + curStage.ToString() + "층-";
    }

    public void SpawnEnemy()
    {
        EnemySpawnManager enemySpawn = curMap.transform.GetChild(0).GetComponent<EnemySpawnManager>();

        for (int i = 0; i < enemySpawn.enemySpawnPoint.Length; i++)
        {
            int curSpawnCode = enemySpawn.enemySpawnPoint[i].monsterCode;
            if (curSpawnCode == 0) return;
            GameObject enemy = pool.EnemyInstantiate("Enemy" + curSpawnCode, enemySpawn.enemySpawnPoint[i].transform.position);
            enemyAmount++;
            enemyList.Add(enemy);
        }
        leftEnemyText.text = "남은 적:" + enemyAmount.ToString();
    }



    public void ClearCheck(GameObject enemy)
    {
        enemyList.Remove(enemy);
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
        PickMap();
        fadeAni.SetBool("Black", false);
    }
}
