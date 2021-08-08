using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicScript : MonoBehaviour
{
    [Header("Managers")]
    public PoolManager pool;
    public StageManager stageManager;

    [Header("Inspector")]
    [SerializeField] Rigidbody2D rigid;
    public Animator animator;

    [Header("Hp")]
    [SerializeField] bool isDie;
    [SerializeField] float curHealth;
    [SerializeField] float maxHealth;

    [Header("Shape")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Target")]
    public GameObject player;
    public bool findPlayer;

    [Header("Move")]
    public bool canMove;
    [SerializeField] int actCode;
    float curMoveSpeed;

    [SerializeField] float freeMoveSpeed;
    [SerializeField] float attackMoveSpeed;
    public float curMoveCool;
    [SerializeField] float curAttackCool;
    [SerializeField] float maxMoveCool;
    [SerializeField] float maxAttackCool;
    private void OnEnable()
    {
        isDie = false;
        findPlayer = false;
        canMove = true;
        curHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            BulletScript bulletScript = collision.GetComponent<BulletScript>();

            if (!bulletScript.isPlayerAttack) return;

            curHealth -= bulletScript.bulletDmg;

            if (curHealth <= 0 && !isDie)
            {
                isDie = true;
                stageManager.monsterAmount--;
                stageManager.ClearCheck();
                gameObject.SetActive(false);
            }
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
            actCode = Random.Range(0, 9);

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
}
