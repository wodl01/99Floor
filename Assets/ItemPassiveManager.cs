using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPassiveManager : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    [SerializeField] AttackJoyPad joyPad;
    [SerializeField] PoolManager pool;

    Transform playerPos;

    public bool Passive_1;
    float passive_1_Cool;
    [SerializeField]float passive_1_Max;

    public bool Passive_2;

    private void Start()
    {
        playerPos = playerState.player.transform;
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
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180), true, false, 1, 1, 1);
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180 + 5), true, false, 1, 1, 0.5f);
                    pool.BulletInstantiate("Bullet1", playerPos.position, Quaternion.Euler(0, 0, joyPad.rotation + 180 - 5), true, false, 1, 1, 0.5f);
                    break;
        }
    }
}
