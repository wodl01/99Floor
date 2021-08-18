using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    public bool isTest;

    [SerializeField] GameObject[] lifeOb;

    [Header("Managers & Scripts")]
    [SerializeField] PlayerState playerState;
    [SerializeField] PoolManager poolManager;

    [Header("Inspector")]
    public Rigidbody2D rigid;

    [Header("Wall")]
    [SerializeField] bool isBorder;

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("WeaponFire")]
    float plusAngle;
    float basicAngle;
    float totalAngle;

    [Header("ThrowEffect")]
    [SerializeField] Sprite[] ThrowEffects1;
    [SerializeField] Sprite[] ThrowEffects2;
    [SerializeField] Sprite[] ThrowEffects3;

    [Header("CurState")]
    public bool isLookLeft;
    private void FixedUpdate()
    {
        Move();
        FarAttack();
    }

    void Move()
    {
        if (!isTest) return;
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        
        rigid.velocity = new Vector2(inputX, inputY) * Time.deltaTime * playerState.moveSpeed * (playerState.moveSpeedPer / 100);



    }

    public void MobileFarAttack(float direction)
    {
        if (playerState.curShotCoolTime <= 0)
        {
            playerState.curShotCoolTime = playerState.totalShotCoolTime * (100 / playerState.attackSpeedPer);

            RandomEffect();
            RandomEffect();

            if (playerState.bulletAmount == 1)
            {
                float dmg = playerState.dmg;
                float speed = 0.15f * (playerState.bulletSpeedPer / 100);
                float destroyTime = (playerState.bulletRangePer / 100);
                poolManager.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0,0, direction + 180), true, false, dmg, destroyTime, speed);

            }
            else
            {
                plusAngle = playerState.bulletSumAngle / (playerState.bulletAmount - 1); //60
                totalAngle = -playerState.bulletSumAngle / 2; //-60

                for (int i = 0; i < playerState.bulletAmount; i++)
                {
                    float dmg = playerState.dmg;
                    float speed = 0.15f * (playerState.bulletSpeedPer / 100);
                    float destroyTime = (playerState.bulletRangePer / 100);
                    poolManager.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, direction + totalAngle + 180), true, false, dmg, destroyTime, speed);


                    totalAngle += plusAngle;
                }
            }
        }
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

            RandomEffect();
            RandomEffect();

            if (playerState.bulletAmount == 1)
            {
                float dmg = playerState.dmg;
                float speed = 0.15f * (playerState.bulletSpeedPer / 100);
                float destroyTime = (playerState.bulletRangePer / 100);
                poolManager.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, basicAngle), true, false, dmg, destroyTime, speed);

            }
            else
            {
                plusAngle = playerState.bulletSumAngle / (playerState.bulletAmount - 1); //60
                totalAngle = -playerState.bulletSumAngle / 2; //-60

                for (int i = 0; i < playerState.bulletAmount; i++)
                {
                    float dmg = playerState.dmg;
                    float speed = 0.15f * (playerState.bulletSpeedPer / 100);
                    float destroyTime = (playerState.bulletRangePer / 100);
                    poolManager.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, basicAngle + totalAngle), true, false, dmg, destroyTime, speed);

                    totalAngle += plusAngle;
                }
            }
        }
    }

    public void RandomEffect()
    {
        int randomNum = Random.Range(0, 3);

        switch (randomNum)
        {
            case 0:
                if (isLookLeft)
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.Euler(0,0,basicAngle), true, false, transform.position, 0.1f, ThrowEffects1).GetComponent<SpriteRenderer>();
                    sprite.flipX = true;
                }
                else
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.Euler(0, 0, basicAngle), true, false, transform.position, 0.1f, ThrowEffects1).GetComponent<SpriteRenderer>();
                    sprite.flipX = false;
                }
                break;
            case 1:
                if (isLookLeft)
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.Euler(0, 0, basicAngle), true, false, transform.position, 0.1f, ThrowEffects2).GetComponent<SpriteRenderer>();
                    sprite.flipX = true;
                }
                else
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.Euler(0, 0, basicAngle), true, false, transform.position, 0.1f, ThrowEffects2).GetComponent<SpriteRenderer>();
                    sprite.flipX = false;
                }
                break;
            case 2:
                if (isLookLeft)
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity, true, false, transform.position, 0.1f, ThrowEffects3).GetComponent<SpriteRenderer>();
                    sprite.flipX = true;
                }
                else
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity, true, false, transform.position, 0.1f, ThrowEffects3).GetComponent<SpriteRenderer>();
                    sprite.flipX = false;
                }
                break;
        }
    }

    public void PlayerHit(int damage)
    {
        int randomNum = Random.Range(0, 101);
        if (randomNum <= playerState.missPer) return;

        playerState.life -= damage;
        for (int i = 0; i < playerState.maxLife; i++)
        {
            lifeOb[i].SetActive(false);
        }
        for (int i = 0; i < playerState.life; i++)
        {
            lifeOb[i].SetActive(true);
        }
        if (playerState.life == 0)
        {

        }
    }
}
