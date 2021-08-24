using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanPlayerScript : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] EnemyBasicScript enemyBasic;

    [Header("MonsterScripts")]
    [SerializeField] Enemy2 enemy2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitBox")
        {
            enemyBasic.findPlayer = true;
        }
    }
}
