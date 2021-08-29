using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitBoxScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] PlayerState playerState;
    [SerializeField] PlayerControlScript player;
    ItemPassiveManager passiveManager;
    [SerializeField] Image[] lifeOb;
    [SerializeField] Sprite lifeOn;
    [SerializeField] Sprite lifeOff;
    [SerializeField] Sprite lifeNull;

    [SerializeField] CapsuleCollider2D hitBox;
    [SerializeField] SpriteRenderer spriteRender;
    [SerializeField] Animator ani;

    [SerializeField] float alpha;

    [SerializeField] bool canHit;

    private void Start()
    {
        passiveManager = ItemPassiveManager.PassiveManager;
    }

    public void PlayerHit(int damage)
    {
        int randomNum = Random.Range(0, 101);
        if (randomNum <= playerState.missPer) return;
        if (!canHit) return;

        playerState.life -= damage;
        PoolManager.pool.DamageInstantiate(transform.position, damage, 0, 0.3f, false);
        LifeIconUpdate();
        StartCoroutine(HitCool());

        if (playerState.life <= 0)
        {
            if (passiveManager.Passive_3)
            {
                passiveManager.Passive_3 = false;
                PlayerHeal(Mathf.CeilToInt(playerState.maxLife / 2));
                return;
            }
            player.canMove = false;
            player.animator.SetBool("Die", true);
            gameManager.GameOver();
        }
    }

    public void PlayerHeal(int heal)
    {
        playerState.life += heal;
        if (playerState.life > playerState.maxLife) playerState.life = playerState.maxLife;

        PoolManager.pool.DamageInstantiate(transform.position, heal, 1, 0.4f, true);

        LifeIconUpdate();
    }

    public void LifeIconUpdate()
    {
        for (int i = 0; i < lifeOb.Length; i++)
        {
            lifeOb[i].sprite = lifeNull;
        }
        for (int i = 0; i < playerState.maxLife; i++)
        {
            lifeOb[i].sprite = lifeOff;
        }
        for (int i = 0; i < playerState.life; i++)
        {
            lifeOb[i].sprite = lifeOn;
        }
    }

    IEnumerator HitCool()
    {
        canHit = false;
        hitBox.enabled = false;
        ani.SetBool("Hit", true);
        yield return new WaitForSeconds(playerState.godTime);
        canHit = true;
        hitBox.enabled = true;
        ani.SetBool("Hit", false);

        spriteRender.color = new Color(1, 1, 1, 1);
    }

    private void Update()
    {
        if (!canHit)
        {
            spriteRender.color = new Color(1, 1, 1, alpha);
        }
    }
}
