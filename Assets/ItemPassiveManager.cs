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
    [SerializeField] Vector2 scale_1;
    [SerializeField] Vector2 boxSize_1;
    float passive_1_Cool;
    [SerializeField]float passive_1_Max;

    public bool Passive_2;//반사딜

    public bool Passive_3;//부활

    public bool Passive_4;//관통샷

    public bool Passive_5;//유도

    private void Awake()
    {
        playerPos = playerState.player.transform;
        PassiveManager = this;
    }

    private void Update()
    {
        if(passive_1_Cool >= 0 && Passive_1)
        {
            passive_1_Cool -= Time.deltaTime;
            if(passive_1_Cool < 0)
            {
                passive_1_Cool = passive_1_Max;
                PassiveActive(0);
            }
        }
    }


    void PassiveActive(int code)
    {
        if(joyPad.isInput)
        switch (code)
        {
            case 0:
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180), true, false, false, scale_1,boxSize_1,1, 0, 1, 0.1f);
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180 + 5), true, false, false, scale_1, boxSize_1, 1, 0, 1, 0.05f);
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180 - 5), true, false, false, scale_1, boxSize_1, 1, 0, 1, 0.05f);
                    break;
        }
    }
}
