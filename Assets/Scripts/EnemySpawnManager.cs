using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Managers")]
    PoolManager pool;
    StageManager stageManager;

    [Header("Enemy")]
    public SpawnPoint[] enemySpawnPoint;

    private void Start()
    {
        pool = PoolManager.pool;
        stageManager = StageManager.stageManager;
    }
}
