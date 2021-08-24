using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
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
        //enemyBasic.pool.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, 45), false, false, 1, 2, 0.13f);
        //enemyBasic.pool.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, 135), false, false, 1, 2, 0.13f);
        //enemyBasic.pool.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, -45), false, false, 1, 2, 0.13f);
        //enemyBasic.pool.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, -135), false, false, 1, 2, 0.13f);
    }
    public void AttackFinish()
    {
        enemyBasic.animator.SetBool("Attack", false);
        enemyBasic.canMove = true;
        //enemyBasic.curMoveCool = 0;
    }
}
