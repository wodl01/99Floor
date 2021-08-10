﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlaceManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] ItemInfoManager itemInfoManager;
    [SerializeField] StageManager stageManager;

    [SerializeField] GameObject normalBox;
    [SerializeField] GameObject[] normalBoxPos;

    public void SpawnBox()
    {
        for (int i = 0; i < normalBoxPos.Length; i++)
        {
            RandomBoxScript box = Instantiate(normalBox, normalBoxPos[i].transform.position, Quaternion.identity).GetComponent<RandomBoxScript>();
            box.itemInfoManager = itemInfoManager;
            box.stageManager = stageManager;
        }
    }
}
