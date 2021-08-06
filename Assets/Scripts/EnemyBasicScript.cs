using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicScript : MonoBehaviour
{
    [Header("Inspector")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;

    [Header("Hp")]
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
    [SerializeField] float curMoveCool;
    [SerializeField] float curAttackCool;
    [SerializeField] float maxMoveCool;
    [SerializeField] float maxAttackCool;
    private void OnEnable()
    {
        curHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            BulletScript bulletScript = collision.GetComponent<BulletScript>();

            if (!bulletScript.isPlayerAttack) return;

            curHealth -= bulletScript.bulletDmg;


            if (curHealth <= 0)
                gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (findPlayer)
        {
            if (curMoveCool >= 0)
                curMoveCool -= Time.deltaTime;
            if (curAttackCool >= 0)
                curAttackCool -= Time.deltaTime;

            if(curAttackCool < 0)
            {
                curAttackCool = maxAttackCool;
                animator.SetBool("Attack",true);
            }

            if (curMoveCool < 0)
            {
                curMoveCool = maxMoveCool;
                actCode = Random.Range(0, 5);

                if (canMove)
                    curMoveSpeed = 0;
                else if (findPlayer)
                    curMoveSpeed = attackMoveSpeed;
                else
                    curMoveSpeed = freeMoveSpeed;

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
                }
            }
        }
        else
        {
            if (curMoveCool >= 0)
                curMoveCool -= Time.deltaTime;

            if (curMoveCool < 0)
            {
                curMoveCool = maxMoveCool;
                actCode = Random.Range(0, 5);
                switch (actCode)
                {
                    case 0:
                        rigid.velocity = new Vector2(0, 0);
                        break;
                    case 1:
                        rigid.velocity = new Vector2(0, freeMoveSpeed);
                        break;
                    case 2:
                        rigid.velocity = new Vector2(0, -freeMoveSpeed);
                        break;
                    case 3:
                        rigid.velocity = new Vector2(freeMoveSpeed, 0);
                        break;
                    case 4:
                        rigid.velocity = new Vector2(-freeMoveSpeed, 0);
                        break;
                }
            }
        }
    }
}
