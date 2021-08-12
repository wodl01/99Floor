using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    [SerializeField] GameObject[] lifeOb;

    [Header("Managers & Scripts")]
    [SerializeField] PlayerState playerState;
    [SerializeField] PoolManager poolManager;

    [Header("Inspector")]
    [SerializeField] Rigidbody2D rigid;

    [Header("Wall")]
    [SerializeField] bool isBorder;

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("WeaponFire")]
    float plusAngle;
    float basicAngle;
    float totalAngle;
    private void FixedUpdate()
    {
        Move();
        FarAttack();
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        
        rigid.velocity = new Vector2(inputX, inputY) * Time.deltaTime * playerState.moveSpeed * (playerState.moveSpeedPer / 100);


        animator.SetInteger("AxisX", (int)inputX);

        animator.SetInteger("AxisY", (int)inputY);
    }

    void FarAttack()
    {
        if (playerState.curShotCoolTime >= 0)
            playerState.curShotCoolTime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.UpArrow))
            basicAngle = 90;
        else if (Input.GetKey(KeyCode.LeftArrow))
            basicAngle = 180;
        else if (Input.GetKey(KeyCode.DownArrow))
            basicAngle = -90;
        else if (Input.GetKey(KeyCode.RightArrow))
            basicAngle = 0;

        if (playerState.curShotCoolTime <= 0 && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            playerState.curShotCoolTime = playerState.totalShotCoolTime * (100 / playerState.attackSpeedPer);

            if (playerState.bulletAmount == 1)
            {
                BulletScript curBullet = poolManager.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, basicAngle)).GetComponent<BulletScript>();
                curBullet.isPlayerAttack = true;
                curBullet.canPassingThrough = false;
                curBullet.bulletDmg = playerState.dmg;
                curBullet.bulletSpeed = 0.15f * (playerState.bulletSpeedPer / 100);
                curBullet.bulletDestroyTime = (playerState.bulletRangePer / 100);
            }
            else
            {
                plusAngle = playerState.bulletSumAngle / (playerState.bulletAmount - 1); //60
                totalAngle = -playerState.bulletSumAngle / 2; //-60

                for (int i = 0; i < playerState.bulletAmount; i++)
                {
                    BulletScript curBullet = poolManager.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, basicAngle + totalAngle)).GetComponent<BulletScript>();
                    curBullet.isPlayerAttack = true;
                    curBullet.canPassingThrough = false;
                    curBullet.bulletDmg = playerState.dmg;
                    curBullet.bulletSpeed = 0.15f * (playerState.bulletSpeedPer / 100);
                    curBullet.bulletDestroyTime = (playerState.bulletRangePer / 100);
                    totalAngle += plusAngle;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            BulletScript hitedBullet = collision.GetComponent<BulletScript>();
            if (hitedBullet.isPlayerAttack) return;

            int randomNum = Random.Range(0, 101);
            if (randomNum <= playerState.missPer) return;

            playerState.life--;
            for (int i = 0; i < playerState.maxLife; i++)
            {
                lifeOb[i].SetActive(false);
            }
            for (int i = 0; i < playerState.life; i++)
            {
                lifeOb[i].SetActive(true);
            }
            if(playerState.life == 0)
            {
                
            }
        }
    }
}
