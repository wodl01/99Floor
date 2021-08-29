using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanPlayerScript : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] EnemyBasicScript enemyBasic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitBox")
        {
            enemyBasic.findPlayer = true;
        }
    }
}
