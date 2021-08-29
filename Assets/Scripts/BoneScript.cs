using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{
    [SerializeField] BossScript boss;
    [SerializeField] EnemyBasicScript enemyScript;

    [SerializeField] int maxHp;
    int curHp;

    [SerializeField] BoxCollider2D bulletCollider;

    [SerializeField] Sprite[] boneEffect;

    [Header("Object")]
    [SerializeField] GameObject redLine;
    [SerializeField] Transform wallPos;

    [Header("State")]
    [SerializeField] Transform followTarget;
    public bool follow;
    public float followSpeed;
    public float angleSpeed;

    public bool shot;
    public float shotSpeed;

    public bool angleToPlayer;
    public bool setPos;

    private void Start()
    {
        redLine.SetActive(false);
    }

    private void Update()
    {
        if (follow)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
            float lerpAngle = Mathf.LerpAngle(transform.eulerAngles.z, followTarget.eulerAngles.z, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
        }
        if (setPos)
            transform.position = followTarget.transform.position;

        if (angleToPlayer)
        {
            float angle = Mathf.Atan2(boss.player.transform.position.y - gameObject.transform.position.y, boss.player.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
            float lerpAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle-270, angleSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
            redLine.SetActive(true);
        }

        if (shot)
        {
            transform.Translate(new Vector3(0, -shotSpeed, 0));
            redLine.SetActive(false);
        }
    }

    public void BulletOn(bool active)
    {
        bulletCollider.enabled = active;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerControlScript>().hitboxScript.PlayerHit(1);
            if (ItemPassiveManager.PassiveManager.Passive_2)
                enemyScript.EnemyHit(20, false);
        }
        if(collision.tag == "Wall")
        {
            shot = false;
            bulletCollider.enabled = false;
            PoolManager.pool.EffectInstantiate("Effect", wallPos.position, Quaternion.identity, new Vector2(3, 3), false, false, new Vector2(0, 0), 0.05f, boneEffect);
        }
    }
}
