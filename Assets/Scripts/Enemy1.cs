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

    public void AttackStart()
    {
        enemyBasic.canMove = false;
    }

    public void Attack1()
    {
        float angle = Mathf.Atan2(enemyBasic.player.transform.position.y - gameObject.transform.position.y, enemyBasic.player.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
        //enemyBasic.pool.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, angle), false, false, 1, 2, 0.2f);
    }
    public void AttackFinish()
    {
        enemyBasic.animator.SetBool("Attack", false);
        enemyBasic.canMove = true;
        //enemyBasic.curMoveCool = 0;
    }
}
