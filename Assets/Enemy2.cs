using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("EnemyBasic")]
    [SerializeField] EnemyBasicScript enemyBasic;

    [Header("Inspector")]
    [SerializeField] Sprite bulletSprite;
    [SerializeField] Vector2 bulletSize;
    [SerializeField] Rigidbody2D rigid;

    [Header("State")]
    [SerializeField] float speed;

    public void AttackStart()
    {
        enemyBasic.canMove = false;
    }

    public void Attack1()
    {
        rigid.AddForce(new Vector2(500, 500));
    }
    public void AttackFinish()
    {
        enemyBasic.animator.SetBool("Attack", false);
        enemyBasic.canMove = true;
    }

    public void Move()
    {

    }
}
