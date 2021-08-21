using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPassiveManager : MonoBehaviour
{
    public static ItemPassiveManager PassiveManager;

    [SerializeField] PlayerState playerState;
    [SerializeField] AttackJoyPad joyPad;
    [SerializeField] PoolManager pool;

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

    public bool Passive_6;//몬스터 폭발
    public int dmg_6;
    public Vector2 scale_6;

    public bool Passive_7;//블랙홀 소환
    [SerializeField] int dmg_7;
    [SerializeField] Vector2 scale_7;
    [SerializeField] float duringTime_7;
    [SerializeField] float passive_7_Cool;
    [SerializeField] float passive_7_Max;

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
                PassiveActive(0);
            }
        }
        if (Passive_7)
        {
            if (passive_7_Cool >= 0)
                passive_7_Cool -= Time.deltaTime;
            if (passive_7_Cool < 0 && joyPad.isInput)
            {
                passive_7_Cool = passive_7_Max;
                PassiveActive(7);
            }
        }
    }


    void PassiveActive(int code)
    {
        switch (code)
        {
            case 0:
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180), sprite_1, true, false, false, scale_1,boxSize_1, dmg_1, 0, 1, 0.8f);
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180 + 5), sprite_1, true, false, false, scale_1, boxSize_1, dmg_1, 0, 1, 0.5f);
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180 - 5), sprite_1, true, false, false, scale_1, boxSize_1, dmg_1, 0, 1, 0.5f);
                    break;
                case 7:
                    GameObject blackHole = pool.BulletInstantiate("BlackHole", playerPos.position, Quaternion.identity, sprite_1, true, true, false, scale_7, new Vector2(0,0), dmg_7, 0, 20, 0);
                    blackHole.transform.GetChild(0).GetComponent<BlackHole>().destroyTime = duringTime_7;
                    Debug.Log("블랙홀");
                    break;
        }
    }
}
