using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] PoolManager pool;
    [SerializeField] StageManager stageManager;

    [Header("Enemy")]
    public SpawnPoint[] enemySpawnPoint;
}
