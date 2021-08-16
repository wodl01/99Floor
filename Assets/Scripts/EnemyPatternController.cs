using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternController : MonoBehaviour
{
    EnemyBasicScript enemyBasic;
    PoolManager pool;
    Rigidbody2D rigid;

    [SerializeField] bool canMoveStartAttack;
    [SerializeField] bool canMoveFinishAttack;

    [SerializeField] bool isShot;
    [SerializeField] bool isSpawn;
    [SerializeField] bool isDash;
    [SerializeField] bool angleToPlayerShot;
    [SerializeField] bool angleToPlayerDash;

    [SerializeField] bool canPassingThrough;
    [SerializeField] int bulletAmount;
    [SerializeField] float[] attackAngle;
    [SerializeField] float bulletSumAngle;
    [SerializeField] float bulletDmg;
    [SerializeField] float bulletDestroyTime;
    [SerializeField] float bulletSpeed;
    [SerializeField] Vector2 bulletSpwanRadius;

    [SerializeField] float dashSpeed;

    float plusAngle;
    float totalAngle;
    float mainAngle;

    private void Start()
    {
        enemyBasic = GetComponent<EnemyBasicScript>();
        pool = enemyBasic.pool;
        rigid = GetComponent<Rigidbody2D>();
    }

    public void AttackStart()
    {
        if (canMoveStartAttack)
            enemyBasic.canMove = true;
        else
            enemyBasic.canMove = false;
    }

    public void Attack()
    {
        if (isSpawn)
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                Vector3 randomRadius = new Vector2(Random.Range(-bulletSpwanRadius.x, bulletSpwanRadius.x), Random.Range(-bulletSpwanRadius.y, bulletSpwanRadius.y));
                pool.BulletInstantiate("Bullet1", transform.position + randomRadius, Quaternion.identity, false, canPassingThrough, bulletDmg, bulletDestroyTime, bulletSpeed);
            }
        }
        else if(isShot)
        {
            if (angleToPlayerShot) mainAngle = Mathf.Atan2(enemyBasic.player.transform.position.y - gameObject.transform.position.y, enemyBasic.player.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
            else mainAngle = 0;

            plusAngle = bulletSumAngle / (bulletAmount - 1);
            totalAngle = -bulletSumAngle / 2;

            if(attackAngle.Length == 1)
                for (int i = 0; i < bulletAmount; i++)
                {
                    pool.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, totalAngle + mainAngle), false, canPassingThrough, bulletDmg, bulletDestroyTime, bulletSpeed);

                    totalAngle += plusAngle;
                }
            else
                for (int i = 0; i < attackAngle.Length; i++)
                    pool.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, attackAngle[i] + mainAngle), false, canPassingThrough, bulletDmg, bulletDestroyTime, bulletSpeed);

        }
    }

    public void Dash()
    {
        if (angleToPlayerDash) mainAngle = Mathf.Atan2(enemyBasic.player.transform.position.y - gameObject.transform.position.y, enemyBasic.player.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
        else mainAngle = 0;

        Vector3 force = Quaternion.AngleAxis(mainAngle, Vector3.forward) * Vector3.right;
        rigid.AddForce(force * dashSpeed);

        if(isDash) enemyBasic.canMove = false;
    }

    public void AttackFinish()
    {
        if (canMoveFinishAttack)
            enemyBasic.canMove = true;
        else
            enemyBasic.canMove = false;

        enemyBasic.animator.SetBool("Attack", false);
    }
}
