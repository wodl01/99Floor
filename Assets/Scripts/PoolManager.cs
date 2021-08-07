using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    [SerializeField] StageManager stageManager;

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

    public GameObject BulletInstantiate(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject curSpawnedOb = poolDictionary[tag].Dequeue();

        curSpawnedOb.transform.position = position;
        curSpawnedOb.transform.rotation = rotation;

        curSpawnedOb.SetActive(true);

        poolDictionary[tag].Enqueue(curSpawnedOb);

        return curSpawnedOb;
    }

    public GameObject EnemyInstantiate(string tag, Vector3 position)
    {
        GameObject curSpawnedOb = poolDictionary[tag].Dequeue();

        curSpawnedOb.transform.position = position;

        curSpawnedOb.SetActive(true);

        poolDictionary[tag].Enqueue(curSpawnedOb);

        return curSpawnedOb;
    }

    public GameObject EffectInstantiate(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject curSpawnedOb = poolDictionary[tag].Dequeue();

        curSpawnedOb.transform.position = position;
        curSpawnedOb.transform.rotation = rotation;

        curSpawnedOb.SetActive(true);

        poolDictionary[tag].Enqueue(curSpawnedOb);

        return curSpawnedOb;
    }
}
