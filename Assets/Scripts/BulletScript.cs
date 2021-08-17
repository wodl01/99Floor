using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Scirpts")]
    public PoolManager poolManager;

    [Header("BulletInfo")]
    public bool canPassingThrough;
    public bool isPlayerAttack;
    public float bulletDmg;
    public float bulletSpeed;
    public float bulletDestroyTime;
    [SerializeField] GameObject hitedOb;

    private void FixedUpdate()
    {
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
        }
        if(collision.tag == "Enemy")
        {
            if (!isPlayerAttack) return;
            if(canPassingThrough) return;

            DestroyBullet();
            hitedOb = collision.gameObject;
            Damaging(0);
        }
        if (collision.tag == "Player")
        {
            if (isPlayerAttack) return;
            if(canPassingThrough) return;

            DestroyBullet();
            hitedOb = collision.gameObject;
            Damaging(1);
        }
        if(collision.tag == "BrokenOb")
        {
            hitedOb = collision.gameObject;
            Damaging(2);
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
                hitedOb.GetComponent<PlayerControlScript>().PlayerHit((int)bulletDmg);
                break;
            case 2:
                BrokenObjectScript ob = hitedOb.GetComponent<BrokenObjectScript>();
                if (!ob.isPassingBy) DestroyBullet();
                ob.BrokenObHit(gameObject, bulletDmg);
                break;
        }
    }

    public void DestroyBullet()
    {
        poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
