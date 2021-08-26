using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject player;

    [SerializeField] int maxHp;
    int curHp;

    [SerializeField] BoneScript[] bones;

    [Header("Pattern")]
    [SerializeField] GameObject dustOb;
    [SerializeField] Animator patternAni;
    [SerializeField] int pattern1AttackCount;
    [SerializeField] int pattern1AttackMaxCount;

    [Header("Particle")]
    [SerializeField] ParticleSystem dustAttack;

    [SerializeField] Animator dustAni;

    private void Start()
    {
        player = PlayerState.playerState.player;
    }

    private void OnEnable()
    {
        curHp = maxHp;

    }
    public void Hit(int dmg)
    {
        curHp -= dmg;
        if(curHp <= 0)
        {
            StageManager.stageManager.StageClear();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            BulletScript bullet = collision.GetComponent<BulletScript>();
            if (!bullet.isPlayerAttack) return;

            bullet.EffectOn();
            bullet.gameObject.SetActive(false);
            Hit((int)bullet.bulletDmg);
        }
    }
    void Idle()
    {
        for (int i = 0; i < bones.Length; i++)
            bones[i].angleToPlayer = false;
        for (int i = 0; i < bones.Length; i++)
            bones[i].follow = true;
        for (int i = 0; i < bones.Length; i++)
            bones[i].shot = false;
        for (int i = 0; i < bones.Length; i++)
            bones[i].BulletOn(false);

        dustOb.SetActive(true);
        pattern1AttackCount = 0;
    }

    IEnumerator Main()
    {
        dustAttack.Play();
        dustAni.SetBool("On", true);

        Idle();
        patternAni.SetInteger("Pattern", 0);
        yield return new WaitForSeconds(4);
        dustAttack.Stop();
        dustAni.SetBool("On", false);

        dustOb.SetActive(false);
        int randomNum = Random.Range(0, 3);
        switch (randomNum)
        {
            case 0:
                StartCoroutine(Pattern_0());
                break;
            case 1:
                StartCoroutine(Pattern_1());
                break;
            case 2:
                StartCoroutine(Pattern_2());
                break;
        }
    }

    IEnumerator Pattern_0()
    {
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].angleToPlayer = true;
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].angleToPlayer = false;
            bones[i].shot = true;
            bones[i].follow = false;
            bones[i].BulletOn(true);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(Main());
    }
    IEnumerator Pattern_1()
    {
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].angleToPlayer = true;
        }
        yield return new WaitForSeconds(3);

        bones[0].shot = true;
        bones[0].follow = false;
        bones[0].angleToPlayer = false;
        bones[0].BulletOn(true);
        yield return new WaitForSeconds(1);

        bones[1].shot = true;
        bones[1].follow = false;
        bones[1].angleToPlayer = false;
        bones[1].BulletOn(true);
        yield return new WaitForSeconds(1);

        bones[2].shot = true;
        bones[2].follow = false;
        bones[2].angleToPlayer = false;
        bones[2].BulletOn(true);
        yield return new WaitForSeconds(1);

        bones[3].shot = true;
        bones[3].follow = false;
        bones[3].angleToPlayer = false;
        bones[3].BulletOn(true);
        yield return new WaitForSeconds(3);

        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].angleToPlayer = true;
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].BulletOn(false);
            bones[i].shot = true;
            bones[i].follow = false;
            bones[i].angleToPlayer = false;
        }
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < bones.Length; i++)
            bones[i].BulletOn(true);

        yield return new WaitForSeconds(3);

        StartCoroutine(Main());
    }

    IEnumerator Pattern_2()
    {
        patternAni.SetInteger("Pattern", 1);
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].follow = true;
            bones[i].BulletOn(false);
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].follow = false;
            bones[i].shot = true;
            bones[i].BulletOn(true);
        }
        yield return new WaitForSeconds(1);
        patternAni.SetInteger("Pattern", 2);
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].follow = true;
            bones[i].BulletOn(false);
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].follow = false;
            bones[i].shot = true;
            bones[i].BulletOn(true);
        }
        yield return new WaitForSeconds(1);

        StartCoroutine(Main());
    }
}
