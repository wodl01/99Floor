using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [Header("EnemyBasic")]
    [SerializeField] EnemyBasicScript enemyBasic;

    [Header("Inspector")]
    [SerializeField] Sprite bulletSprite;
    [SerializeField] Vector2 bulletSize;
    [SerializeField] Rigidbody2D rigid;

    [Header("State")]
    [SerializeField] float speed;

    public void FireToPlayer()
    {
        enemyBasic.canMove = false;
    }

    /*void FollowTarget()
    {
        Vector2 followVector = Vector2.MoveTowards(transform.position, enemyBasic.player.transform.position, speed * Time.deltaTime);
        rigid.MovePosition(followVector);
    }*/
}
