using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Scirpts")]
    public PoolManager poolManager;
    public ItemPassiveManager passive;

    [Header("Inspector")]
    [SerializeField] Rigidbody2D rigid;
    public SpriteRenderer spriteRender;
    public BoxCollider2D boxCol;

    [Header("BulletInfo")]
    public GameObject bulletHost;
    public GameObject target;
    public bool canPassingThrough;
    public bool isPlayerAttack;
    public bool isHoming;
    public float bulletDmg;
    public float bulletSpeed;
    public float homingPower;
    public float bulletDestroyTime;
    [SerializeField] GameObject hitedOb;

    [Header("Test")]
    [SerializeField] Sprite[] destroyEffects;


    private void FixedUpdate()
    {
        if (isHoming && target != null)
        {
            Vector2 direction = transform.position - target.transform.position;

            direction.Normalize();

            float cross = Vector3.Cross(direction, transform.right).z;

            rigid.angularVelocity = cross * homingPower;
        }

        if (bulletDestroyTime > 0)
        {
            bulletDestroyTime -= Time.deltaTime;
            if (bulletDestroyTime < 0)
                gameObject.SetActive(false);
        }

        transform.Translate(new Vector3(bulletSpeed, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            if(canPassingThrough) return;
            DestroyBullet();
            EffectOn();
        }
        if(collision.tag == "Enemy")
        {
            if (!isPlayerAttack) return;

            if(!canPassingThrough)
            DestroyBullet();

            if (passive.Passive_8 && passive.passive_8_Cool < 0)
                passive.PassiveActive(8,transform.position);
                
            hitedOb = collision.gameObject;
            Damaging(0);
            EffectOn();
        }
        if (collision.tag == "PlayerHitBox")
        {
            if (isPlayerAttack) return;
            if(canPassingThrough) return;

            DestroyBullet();
            hitedOb = collision.gameObject;
            Damaging(1);
            EffectOn();
        }
        if(collision.tag == "BrokenOb")
        {
            DestroyBullet();
            hitedOb = collision.gameObject;
            Damaging(2);
            EffectOn();
        }
    }

    public void Damaging(int targetNum)
    {
        switch (targetNum)
        {
            case 0:
                EnemyBasicScript enemy = hitedOb.GetComponent<EnemyBasicScript>();
                enemy.EnemyHit(bulletDmg);
                break;
            case 1:
                hitedOb.gameObject.GetComponent<PlayerHitBoxScript>().PlayerHit((int)bulletDmg);
                if (passive.Passive_2)
                    bulletHost.GetComponent<EnemyBasicScript>().EnemyHit(20);
                break;
            case 2:
                BrokenObjectScript ob = hitedOb.GetComponent<BrokenObjectScript>();
                ob.BrokenObHit(gameObject, 0, bulletDmg);
                break;
        }
    }

    public void DestroyBullet()
    {
        gameObject.SetActive(false);
    }

    public void EffectOn()
    {
        poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity, new Vector2(0.8f, 0.8f), false, true, new Vector2(0, 0), 0.05f, destroyEffects);
    }
}
