using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] PoolManager pool;

    [SerializeField] int curStage;
    [SerializeField] int maxStage;

    [SerializeField] SpawnPoint[] enemySpawnPoint;
    public int monsterAmount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) StageStart();
    }

    void StageStart()
    {
        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {
            int curSpawnCode = enemySpawnPoint[i].monsterCode;
            if (curSpawnCode == 0) return;
            pool.EnemyInstantiate("Enemy" + curSpawnCode, enemySpawnPoint[i].transform.position);
            monsterAmount++;
        }
    }

    public void ClearCheck()
    {
        if(monsterAmount == 0)
        {
            StageStart();
        }
    }
}
