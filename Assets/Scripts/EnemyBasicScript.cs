using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicScript : MonoBehaviour
{
    [Header("Managers")]
    public PoolManager pool;
    public StageManager stageManager;
    public ItemPassiveManager passive;

    [Header("Inspector")]
    public Rigidbody2D rigid;
    public Animator animator;

    [Header("Hp")]
    [SerializeField] bool isDie;
    [SerializeField] float curHealth;
    [SerializeField] float maxHealth;

    [Header("Shape")]
    //[SerializeField] SpriteRenderer spriteRenderer;

    [Header("Target")]
    public GameObject player;
    public bool findPlayer;

    [Header("Move")]
    public bool canMove;
    [SerializeField] bool goDiagonal;
    [SerializeField] int actCode;
    float curMoveSpeed;

    [SerializeField] float freeMoveSpeed;
    [SerializeField] float attackMoveSpeed;
    public float curMoveCool;
    [SerializeField] float curAttackCool;
    [SerializeField] float maxMoveCool;
    [SerializeField] float maxAttackCool;

    [Header("Attack Info")]
    public bool isSpecialBullet;
    public Sprite attackSprite;
    public Vector2 attackSize;
    public Vector2 attackBoxSize;
    public Vector2 attackBoxOffset;

    private void OnEnable()
    {
        isDie = false;
        findPlayer = false;
        canMove = true;
        curHealth = maxHealth;
    }

    public void EnemyHit(float damage)
    {
        curHealth -= damage;

        findPlayer = true;

        if (curHealth <= 0 && !isDie)
        {
            isDie = true;
            stageManager.enemyAmount--;

            if (passive.Passive_6) pool.BulletInstantiate("Explosion_1", transform.position, Quaternion.identity, null, true, true, false, passive.scale_6, new Vector2(1, 1), passive.dmg_6, 0, 10, 0);
            
            stageManager.ClearCheck();
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (curMoveCool >= 0)
            curMoveCool -= Time.deltaTime;
        if (curAttackCool >= 0)
            curAttackCool -= Time.deltaTime;

        if (curAttackCool < 0 && findPlayer)
        {
            curAttackCool = maxAttackCool;
            rigid.velocity = new Vector2(0, 0);
            animator.SetBool("Attack", true);
        }

        if (curMoveCool < 0)
        {
            curMoveCool = maxMoveCool;
            actCode = Random.Range(0, goDiagonal ? 9 : 5);

            if (findPlayer)
                curMoveSpeed = attackMoveSpeed;
            else
                curMoveSpeed = freeMoveSpeed;

            animator.SetBool("Move", true);
            if (!canMove)
            {
                animator.SetBool("Move", false);
                return;
            }

            switch (actCode)
            {
                case 0:
                    rigid.velocity = new Vector2(0, 0);
                    break;
                case 1:
                    rigid.velocity = new Vector2(0, curMoveSpeed);
                    break;
                case 2:
                    rigid.velocity = new Vector2(0, -curMoveSpeed);
                    break;
                case 3:
                    rigid.velocity = new Vector2(curMoveSpeed, 0);
                    break;
                case 4:
                    rigid.velocity = new Vector2(-curMoveSpeed, 0);
                    break;
                case 5:
                    rigid.velocity = new Vector2(curMoveSpeed, curMoveSpeed);
                    break;
                case 6:
                    rigid.velocity = new Vector2(curMoveSpeed, -curMoveSpeed);
                    break;
                case 7:
                    rigid.velocity = new Vector2(-curMoveSpeed, curMoveSpeed);
                    break;
                case 8:
                    rigid.velocity = new Vector2(-curMoveSpeed, -curMoveSpeed);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            findPlayer = true;
        }
    }
}
