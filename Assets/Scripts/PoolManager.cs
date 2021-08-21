using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    [SerializeField] PlayerState playerState;
    [SerializeField] StageManager stageManager;
    [SerializeField] ItemPassiveManager passiveManager;

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int amount;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    private void Start()
    {
        PrePoolInstantiate();
    }

    public void PrePoolInstantiate()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.amount; i++)
            {
                GameObject curObj = Instantiate(pool.prefab, new Vector3(16, 16, 0), Quaternion.identity);

                if (curObj.tag == "Bullet")
                    curObj.GetComponent<BulletScript>().poolManager = this;

                if (curObj.tag == "Enemy")
                {
                    EnemyBasicScript enemy = curObj.GetComponent<EnemyBasicScript>();
                    enemy.pool = this;
                    enemy.stageManager = stageManager;
                    enemy.player = playerState.player;
                }


                curObj.SetActive(false);
                objectPool.Enqueue(curObj);
            }
            poolDictionary.Add(pool.name, objectPool);
        }
    }

    public GameObject BulletInstantiate(string tag, Vector3 position, Quaternion rotation, Sprite sprite, bool isPlayerAttack, bool canPassingThrough, bool isHoming, Vector2 scale, Vector2 boxSize, float dmg, float homingPower, float distroyTime, float bulletSpeed)
    {
        BulletScript curSpawnedOb = poolDictionary[tag].Dequeue().GetComponent<BulletScript>();

        curSpawnedOb.passive = passiveManager;

        if(sprite != null)
        curSpawnedOb.spriteRender.sprite = sprite;
        curSpawnedOb.transform.position = position;
        curSpawnedOb.transform.rotation = rotation;
        curSpawnedOb.transform.localScale = scale;

        if(curSpawnedOb.boxCol == true)
        curSpawnedOb.boxCol.size = boxSize;

        curSpawnedOb.isPlayerAttack = isPlayerAttack;
        curSpawnedOb.isHoming = isHoming;
        curSpawnedOb.canPassingThrough = canPassingThrough;
        curSpawnedOb.bulletDmg = dmg;
        curSpawnedOb.homingPower = homingPower;
        curSpawnedOb.bulletDestroyTime = distroyTime;
        curSpawnedOb.bulletSpeed = bulletSpeed;

        curSpawnedOb.target = null;
        if (!isPlayerAttack) curSpawnedOb.target = playerState.player;

        curSpawnedOb.gameObject.SetActive(true);

        poolDictionary[tag].Enqueue(curSpawnedOb.gameObject);

        return curSpawnedOb.gameObject;
    }

    public GameObject EnemyInstantiate(string tag, Vector3 position)
    {
        GameObject curSpawnedOb = poolDictionary[tag].Dequeue();

        curSpawnedOb.GetComponent<EnemyBasicScript>().passive = passiveManager;
        curSpawnedOb.GetComponent<EnemyBasicScript>().pool = this;

        curSpawnedOb.transform.position = position;

        curSpawnedOb.SetActive(true);

        poolDictionary[tag].Enqueue(curSpawnedOb);

        return curSpawnedOb;
    }

    public GameObject EffectInstantiate(string tag, Vector3 position, Quaternion rotation, Vector2 scale, bool isFollow, bool isReveling, Vector2 followPos, float spriteCycleTime, Sprite[] sprites)
    {
        EffectManager curSpawnedOb = poolDictionary[tag].Dequeue().GetComponent<EffectManager>();

        curSpawnedOb.isFollow = isFollow;
        curSpawnedOb.isRevealing = isReveling;
        curSpawnedOb.followPos = followPos;
        curSpawnedOb.spriteCycleTime = spriteCycleTime;
        curSpawnedOb.maxSpriteNum = sprites.Length;

        for (int i = 0; i < sprites.Length; i++) curSpawnedOb.effectSprites[i] = sprites[i];

        curSpawnedOb.transform.position = position;
        curSpawnedOb.transform.rotation = rotation;
        curSpawnedOb.transform.localScale = scale;

        curSpawnedOb.gameObject.SetActive(true);

        poolDictionary[tag].Enqueue(curSpawnedOb.gameObject);

        return curSpawnedOb.gameObject;
    }
}
