using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxScript : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    ItemPassiveManager passiveManager;
    [SerializeField] GameObject[] lifeOb;

    [SerializeField] CircleCollider2D hitBox;
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
        if (playerState.life == 0) return;
        if (!canHit) return;

        playerState.life -= damage;
        LifeIconUpdate();
        StartCoroutine(HitCool());

        if (playerState.life == 0)
        {
            if (passiveManager.Passive_3)
            {
                passiveManager.Passive_3 = false;
                PlayerHeal(Mathf.CeilToInt(playerState.maxLife / 2));
                return;
            }
        }
    }

    public void PlayerHeal(int heal)
    {
        playerState.life += heal;
        if (playerState.life > playerState.maxLife) playerState.life = playerState.maxLife;

        LifeIconUpdate();
    }

    void LifeIconUpdate()
    {
        for (int i = 0; i < playerState.maxLife; i++)
        {
            lifeOb[i].SetActive(false);
        }
        for (int i = 0; i < playerState.life; i++)
        {
            lifeOb[i].SetActive(true);
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

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PublicDamageBullet")
        {
            PlayerHit((int)collision.GetComponent<GroupBulletManager>().playerDamage);
        }
    }*/
}
