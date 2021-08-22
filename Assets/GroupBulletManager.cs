using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBulletManager : MonoBehaviour
{
    public int playerDamage;
    public float enemyDamage;


    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerControlScript>().hitboxScript.PlayerHit(playerDamage);
        }
        else if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyBasicScript>().EnemyHit(enemyDamage);
        }
        else if(collision.tag == "BrokenOb")
        {
            collision.GetComponent<BrokenObjectScript>().BrokenObHit(gameObject, 1, enemyDamage);
        }
    }
}
