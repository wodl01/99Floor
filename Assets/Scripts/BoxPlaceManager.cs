using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlaceManager : MonoBehaviour
{
    [Header("Manager")]
    ItemInfoManager itemInfoManager;
    StageManager stageManager;

    [Header("Box")]
    [SerializeField] GameObject normalBox;
    [SerializeField] GameObject grade_2;
    [SerializeField] GameObject grade_1;
    [SerializeField] GameObject grade_Devil;
    [SerializeField] GameObject grade_Holy;
    [SerializeField] GameObject[] normalBoxPos;

    [Header("Door")]
    [SerializeField] GameObject door;
    [SerializeField] GameObject doorPos;

    GameObject box;

    private void Start()
    {
        itemInfoManager = ItemInfoManager.itemInfo;
        stageManager = StageManager.stageManager;
    }

    public void PlaceObject()
    {
        SpawnBox();
        SpawnDoor();
    }

    public void SpawnDoor()
    {
        door.transform.position = doorPos.transform.position - new Vector3(0, 2.08f, 0);
    }

    public void SpawnBox()
    {
        for (int i = 0; i < normalBoxPos.Length; i++)
        {
            switch (normalBoxPos[i].GetComponent<BoxPoint>().boxCode)
            {
                case 0:
                    box = normalBox;
                    break;
                case 1:
                    box = grade_2;
                    break;
                case 2:
                    box = grade_1;
                    break;
                case 3:
                    box = grade_Devil;
                    break;
                case 4:
                    box = grade_Holy;
                    break;
            }
            RandomBoxScript curBox = PoolManager.pool.PoolInstantiate(box, normalBoxPos[i].transform, Quaternion.identity).GetComponent<RandomBoxScript>();
            curBox.itemInfoManager = itemInfoManager;
            curBox.stageManager = stageManager;
        }
    }
}
