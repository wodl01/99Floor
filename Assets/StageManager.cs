using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] PoolManager pool;

    [SerializeField] int curStage;
    [SerializeField] int maxStage;

    [Header("Enemy")]
    [SerializeField] SpawnPoint[] enemySpawnPoint;
    public int enemyAmount;

    [Header("Ui")]
    [SerializeField] Text curStageText;
    [SerializeField] Text leftEnemyText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) StageStart();
    }

    void StageStart()
    {
        curStage++;
        curStageText.text = "-" + curStage.ToString() + "층-";
        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {
            int curSpawnCode = enemySpawnPoint[i].monsterCode;
            if (curSpawnCode == 0) return;
            pool.EnemyInstantiate("Enemy" + curSpawnCode, enemySpawnPoint[i].transform.position);
            enemyAmount++;
        }
        leftEnemyText.text = "남은 적:" + enemyAmount.ToString();
    }

    public void ClearCheck()
    {
        leftEnemyText.text = "남은 적:" + enemyAmount.ToString();
        if(enemyAmount == 0)
        {
            StageStart();
        }
    }
}
