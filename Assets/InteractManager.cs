using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager inter;

    public int interactNum;
    public GameObject interactOb;

    [SerializeField] GameObject buttonA;
    [SerializeField] GameObject buttonB;
    public GameObject buttonC;


    private void Awake()
    {
        inter = this;
        buttonA.SetActive(false);
        buttonB.SetActive(false);
    }

    public void SetInfo(int interNum, GameObject interOb, bool A, bool B)
    {
        interactNum = interNum;
        interactOb = interOb;

        buttonA.SetActive(A);
        buttonB.SetActive(B);
    }


    public void PushA()
    {
        switch (interactNum)
        {
            case 0:
                RandomBoxScript box0 = interactOb.GetComponent<RandomBoxScript>();
                box0.OpenBox();
                break;
            case 1:
                RandomBoxScript box1 = interactOb.GetComponent<RandomBoxScript>();
                box1.GetItem();
                break;
            case 2:
                Teleport tel = interactOb.GetComponent<Teleport>();
                tel.Tel();
                break;
            case 3:
                Shop shopItem = interactOb.GetComponent<Shop>();
                shopItem.BuyItem();
                break;
            case 4:
                DoorScript door = interactOb.GetComponent<DoorScript>();
                door.UseDoor();
                break;
        }
    }
    public void PushB()
    {
        switch (interactNum)
        {
            case 1:
                RandomBoxScript box0 = interactOb.GetComponent<RandomBoxScript>();
                box0.SellItem();
                break;
        }
    }
}
