using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Scirpts")]
    public PoolManager poolManager;

    [Header("BulletInfo")]
    public bool isPlayerAttack;
    public float bulletDmg;
    public float bulletSpeed;
    public float bulletDestroyTime;


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
            gameObject.SetActive(false);
            poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity);
        }
        if(collision.tag == "Enemy")
        {
            if (!isPlayerAttack) return;
            gameObject.SetActive(false);
            poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity);
        }
        if (collision.tag == "Player")
        {
            if (isPlayerAttack) return;
            gameObject.SetActive(false);
            poolManager.EffectInstantiate("Effect", transform.position, Quaternion.identity);
        }
    }
}
