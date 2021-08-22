using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlaceManager : MonoBehaviour
{
    [Header("Manager")]
    ItemInfoManager itemInfoManager;
    StageManager stageManager;

    [SerializeField] List<GameObject> boxes;

    [Header("Box")]
    [SerializeField] GameObject normalBox;
    [SerializeField] GameObject[] normalBoxPos;

    [Header("Door")]
    [SerializeField] GameObject door;
    [SerializeField] GameObject doorPos;

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
        door.transform.position = doorPos.transform.position - new Vector3(0, 2.115f, 0);
    }

    public void SpawnBox()
    {
        for (int i = 0; i < normalBoxPos.Length; i++)
        {
            RandomBoxScript box = Instantiate(normalBox, normalBoxPos[i].transform.position, Quaternion.identity).GetComponent<RandomBoxScript>();
            box.itemInfoManager = itemInfoManager;
            box.stageManager = stageManager;

            boxes.Add(box.gameObject);
        }
    }

    public void ClearBox()
    {
        for (int i = 0; i < boxes.Count; i++)
            Destroy(boxes[i]);
    }
}
