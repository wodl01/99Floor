using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState playerState;
    public GameObject player;

    private void Start() => playerState = this;

    [Header("Player Life")]
    public int maxLife;
    public int life;

    [Header("PlayerMove")]
    public float moveSpeed;
    public float moveSpeedPer;

    [Header("Goods")]
    public int gold;

    [Header("Bullet Basic")]
    public float curShotCoolTime;
    public float totalShotCoolTime;
    public float bulletDestroyTime;

    [Header("Player State")]
    public float attackSpeedPer;
    public float dmg;
    public float bulletAmount;
    public float bulletSumAngle;
    public float bulletSpeedPer;
    public float bulletRangePer;
    public int missPer;
    public int godTime;
}
