using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPassiveManager : MonoBehaviour
{
    public static ItemPassiveManager PassiveManager;

    [SerializeField] PlayerState playerState;
    [SerializeField] AttackJoyPad joyPad;
    [SerializeField] PoolManager pool;
    [SerializeField] StageManager stageManager;
    [SerializeField] PlayerControlScript playerScript;

    Transform playerPos;

    [Header("PassiveState")]
    public bool Passive_1; //추가타
    [SerializeField] int dmg_1;
    [SerializeField] Sprite sprite_1;
    [SerializeField] Vector2 scale_1;
    [SerializeField] Vector2 boxSize_1;
    float passive_1_Cool;
    [SerializeField]float passive_1_Max;

    public bool Passive_2;//반사딜

    public bool Passive_3;//부활

    public bool Passive_4;//관통샷

    public bool Passive_5;//유도
    public float Passive_5Power;

    public bool Passive_6;//몬스터 폭발
    public int dmg_6;
    public Vector2 scale_6;

    public bool Passive_7;//블랙홀 소환
    [SerializeField] int dmg_7;
    [SerializeField] Vector2 scale_7;
    [SerializeField] float duringTime_7;
    [SerializeField] float passive_7_Cool;
    [SerializeField] float passive_7_Max;

    public bool Passive_8;//운석 소환
    [SerializeField] int dmg_8;
    [SerializeField] Vector2 scale_8;
    public float passive_8_Cool;
    [SerializeField] float passive_8_Max;

    public bool Passive_9; //구르기 공격
    [SerializeField] int dmg_9;
    [SerializeField] Sprite sprite_9;
    [SerializeField] Vector2 scale_9;
    [SerializeField] Vector2 boxSize_9;
    [SerializeField] float speed_9;

    public bool Passive_10; //즉사 총알
    [SerializeField] int Passive_Dmg_10;
    [SerializeField] int Passive_10_Per;



    private void Awake()
    {
        playerPos = playerState.player.transform;
        PassiveManager = this;
    }

    private void Update()
    {
        if(Passive_1)
        {
            if(passive_1_Cool >= 0)
            passive_1_Cool -= Time.deltaTime;
            if(passive_1_Cool < 0 && joyPad.isInput)
            {
                passive_1_Cool = passive_1_Max;
                PassiveActive(1, new Vector2(0,0), gameObject);
            }
        }
        if (Passive_7)
        {
            if (passive_7_Cool >= 0)
                passive_7_Cool -= Time.deltaTime;
            if (passive_7_Cool < 0 && joyPad.isInput)
            {
                passive_7_Cool = passive_7_Max;
                PassiveActive(7, new Vector2(0, 0),gameObject);
            }
        }
        if (Passive_8)
        {
            if (passive_8_Cool >= 0)
                passive_8_Cool -= Time.deltaTime;
        }
    }


    public void PassiveActive(int code, Vector2 pos, GameObject objectTarget)
    {
        switch (code)
        {
            case 1:
                passive_1_Cool = passive_1_Max;
                pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180), sprite_1, true, false, false, scale_1,boxSize_1, dmg_1, 0, 1, 0.8f);
                pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180 + 5), sprite_1, true, false, false, scale_1, boxSize_1, dmg_1, 0, 1, 0.5f);
                pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180 - 5), sprite_1, true, false, false, scale_1, boxSize_1, dmg_1, 0, 1, 0.5f);
                break;
            case 7:
                GameObject blackHole = pool.BulletInstantiate("BlackHole", playerPos.position, Quaternion.identity, sprite_1, true, true, false, scale_7, new Vector2(0, 0), dmg_7, 0, 20, 0);
                blackHole.transform.GetChild(0).GetComponent<BlackHole>().destroyTime = duringTime_7;
                Debug.Log("블랙홀");
                break;
            case 8:
                passive_8_Cool = passive_8_Max;
                pool.BulletInstantiate("Bullet3", pos, Quaternion.identity, null, true, true, false, scale_8, new Vector2(0, 0), dmg_8, 0, 10, 0);
                break;
            case 9:
                float mainAngle = Mathf.Atan2(playerPos.position.y - pos.y, playerPos.position.x - pos.x) * Mathf.Rad2Deg;
                pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, mainAngle + 180), sprite_9, true, false, false, scale_9, boxSize_9, dmg_9, 0, 0.6f, speed_9);
                pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, mainAngle + 180 + 8), sprite_9, true, false, false, scale_9, boxSize_9, dmg_9, 0, 0.6f, speed_9);
                pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, mainAngle + 180 - 8), sprite_9, true, false, false, scale_9, boxSize_9, dmg_9, 0, 0.6f, speed_9);
                break;
            case 10:
                if(Passive_10_Per > Random.Range(0, 100))
                {
                    objectTarget.GetComponent<BulletScript>().bulletDmg = Passive_Dmg_10;
                    objectTarget.GetComponent<BulletScript>().isInstantDeath = true;
                    pool.BulletEffectInstantiate("BulletEffect1", objectTarget.transform.position, objectTarget);
                } 
                break;
        }
    }

    float shortDis;
    GameObject enemy;
    public GameObject NearestObFinder()
    {
        if (stageManager.enemyList.Count == 0) return playerScript.shotPos.gameObject;

        List<GameObject> enemys = stageManager.enemyList;

        enemy = enemys[0]; // 첫번째를 먼저
        shortDis = Vector3.Distance(gameObject.transform.position, enemys[0].transform.position);

        foreach (GameObject found in enemys)
        {
            float Distance = Vector3.Distance(playerPos.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                enemy = found;
                shortDis = Distance;
            }
        }
        return enemy;
    }
}
