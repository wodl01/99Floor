using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    public bool isTest;

    [Header("Managers & Scripts")]
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerState playerState;
    [SerializeField] PoolManager poolManager;
    [SerializeField] ItemPassiveManager passive;
    [SerializeField] PlayerAniManager playerAni;
    [SerializeField] InventoryManager inventory;
    public PlayerHitBoxScript hitboxScript;

    [Header("Inspector")]
    public Rigidbody2D rigid;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] CapsuleCollider2D hitBox;

    [Header("Wall")]
    [SerializeField] bool isBorder;

    [Header("Animation")]
    public Animator animator;

    [Header("WeaponFire")]
    public Transform shotPosMid;
    public Transform shotPos;
    public SpriteRenderer weaponSpriteRender;
    [SerializeField] Sprite weapon;
    [SerializeField] Sprite nullsprite;
    [SerializeField] GameObject bomb;
    [SerializeField] float bombThrowPower;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] Vector2 bulletScale;
    [SerializeField] Vector2 boxSize;
    float plusAngle;
    float basicAngle;
    float totalAngle;

    [Header("ThrowEffect")]
    [SerializeField] Sprite[] ThrowEffects1;
    [SerializeField] Sprite[] ThrowEffects2;
    [SerializeField] Sprite[] ThrowEffects3;

    [Header("Roll")]
    [SerializeField] float rollCoolTime;
    [SerializeField] float curRollCool;
    [SerializeField] float rollDuringTime;
    [SerializeField] float rollPower;

    [Header("CurState")]
    public bool canMove;
    public bool dashing;
    public bool canAttack;
    public bool isLookLeft;
    private void FixedUpdate()
    {
        MoveTest();
        FarAttack();

        if(curRollCool >= 0) curRollCool -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) InputRollBtn();
        if (Input.GetKeyDown(KeyCode.R)) UseBomb();
    }

    void MoveTest()
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
            if (!canAttack) return;

            SoundManager.Play("Attack_P");
            playerState.curShotCoolTime = playerState.totalShotCoolTime * (100 / playerState.attackSpeedPer);

            RandomEffect();
            RandomEffect();

            if (playerState.bulletAmount == 1)
            {
                float dmg = playerState.dmg;
                float speed = 0.15f * (playerState.bulletSpeedPer / 100);
                float destroyTime = (playerState.bulletRangePer / 100);
                GameObject bullet = poolManager.BulletInstantiate("Bullet1", shotPos.position, Quaternion.Euler(0,0, direction + 180), bulletSprite, true, passive.Passive_4, passive.Passive_5, bulletScale, boxSize, dmg, passive.Passive_5Power, destroyTime, speed);
                if (passive.Passive_10)
                    passive.PassiveActive(10, transform.position, bullet);
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
                    GameObject bullet = poolManager.BulletInstantiate("Bullet1", shotPos.position, Quaternion.Euler(0, 0, direction + totalAngle + 180), bulletSprite, true, passive.Passive_4,passive.Passive_5,bulletScale, boxSize, dmg, passive.Passive_5Power, destroyTime, speed);
                    if (passive.Passive_10)
                        passive.PassiveActive(10, transform.position, bullet);

                    totalAngle += plusAngle;
                }
            }
        }
    }

    void FarAttack()
    {
        if (playerState.curShotCoolTime >= 0)
        {
            playerState.curShotCoolTime -= Time.deltaTime;
            weaponSpriteRender.sprite = nullsprite;
            if (playerState.curShotCoolTime < 0)
                weaponSpriteRender.sprite = weapon;
        }


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
                poolManager.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, basicAngle), bulletSprite, true, passive.Passive_4, passive.Passive_5, bulletScale, boxSize, dmg, passive.Passive_5Power, destroyTime, speed);

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
                    poolManager.BulletInstantiate("Bullet1", transform.position, Quaternion.Euler(0, 0, basicAngle + totalAngle), bulletSprite, true, passive.Passive_4, passive.Passive_5, bulletScale, boxSize, dmg, passive.Passive_5Power, destroyTime, speed);

                    totalAngle += plusAngle;
                }
            }
        }
    }

    public void InputRollBtn()
    {
        if (curRollCool > 0 || !playerAni.movePadActive) return;
        SoundManager.Play("Roll");
        StartCoroutine(Roll());
        curRollCool = rollCoolTime;
    }

    IEnumerator Roll()
    {
        canMove = false;
        dashing = true;
        rigid.velocity = new Vector2(0, 0);
        Vector3 force = Quaternion.AngleAxis(playerAni.moveRotate, Vector3.forward) * Vector3.right;
        rigid.AddForce(-force * ((playerState.moveSpeedPer * rollPower) / 100));

        animator.SetBool("Roll", true);
        hitBox.enabled = false;
        sprite.flipX = !isLookLeft;

        yield return new WaitForSeconds(rollDuringTime);
        canMove = true;
        dashing = false;

        animator.SetBool("Roll", false);
        hitBox.enabled = true;

        if (passive.Passive_9)
            passive.PassiveActive(9, passive.NearestObFinder().transform.position, gameObject);
    }

    public void UseBomb()
    {
        if (playerState.bomb > 0)
        {
            GameObject bombOb = poolManager.PoolInstantiate(bomb, shotPos, Quaternion.identity);
            if (playerAni.movePadActive)
            {
                Vector3 force = Quaternion.AngleAxis(playerAni.moveRotate, Vector3.forward) * Vector3.right;
                bombOb.GetComponent<Rigidbody2D>().AddForce(-force * bombThrowPower);
            }
        }
        inventory.BombIconUpdate(-1);
    }

    public void Sound(int index)
    {
        switch (index)
        {
            case 0:
                if (playerAni.attackPadActive) return;
                int randomNum = Random.Range(0, 4);
                switch (randomNum)
                {
                    case 0:
                        SoundManager.Play("FootStep1");
                        break;
                    case 1:
                        SoundManager.Play("FootStep2");
                        break;
                    case 2:
                        SoundManager.Play("FootStep3");
                        break;
                    case 3:
                        SoundManager.Play("FootStep4");
                        break;
                }
                break;
            case 1:
                SoundManager.Play("Roll");
                break;
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
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.Euler(0,0,basicAngle), new Vector2(0.35f, 0.35f), true, false, transform.position, 0.1f, ThrowEffects1).GetComponent<SpriteRenderer>();
                    sprite.flipX = true;
                }
                else
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.Euler(0, 0, basicAngle), new Vector2(0.35f, 0.35f), true, false, transform.position, 0.1f, ThrowEffects1).GetComponent<SpriteRenderer>();
                    sprite.flipX = false;
                }
                break;
            case 1:
                if (isLookLeft)
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.Euler(0, 0, basicAngle), new Vector2(0.35f, 0.35f), true, false, transform.position, 0.1f, ThrowEffects2).GetComponent<SpriteRenderer>();
                    sprite.flipX = true;
                }
                else
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.Euler(0, 0, basicAngle), new Vector2(0.35f, 0.35f), true, false, transform.position, 0.1f, ThrowEffects2).GetComponent<SpriteRenderer>();
                    sprite.flipX = false;
                }
                break;
            case 2:
                if (isLookLeft)
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity, new Vector2(0.35f, 0.35f), true, false, transform.position, 0.1f, ThrowEffects3).GetComponent<SpriteRenderer>();
                    sprite.flipX = true;
                }
                else
                {
                    SpriteRenderer sprite = poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity, new Vector2(0.35f, 0.35f), true, false, transform.position, 0.1f, ThrowEffects3).GetComponent<SpriteRenderer>();
                    sprite.flipX = false;
                }
                break;
        }
    }
}
