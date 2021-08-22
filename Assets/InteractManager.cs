using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour
{
    public static InteractManager inter;

    [SerializeField] Text warningText;

    public int interactNum;
    GameObject interactOb;

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
            case 5:
                Shop shop = interactOb.GetComponent<Shop>();
                shop.BuyItem();
                break;
            case 6:
                GargoyleScript gargoyle = interactOb.GetComponent<GargoyleScript>();
                gargoyle.InteractGagoyle();
                break;
            case 7:
                MysteryRibber river = interactOb.GetComponent<MysteryRibber>();
                river.UseRiver();
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

    public IEnumerator WarningText(int warningCode)
    {
        if (!warningText.gameObject.activeSelf)
        {
            switch (warningCode)
            {
                case 0:
                    warningText.text = "적을 모두 무찌르십시오.";
                    break;
                case 1:
                    warningText.text = "스테이지를 클리어 해주십시오.";
                    break;
                case 2:
                    warningText.text = "Gold가 부족합니다.";
                    break;
                case 3:
                    warningText.text = "상자를 열기위한 열쇠가 부족합니다.";
                    break;
                case 4:
                    warningText.text = "공물이 부족합니다.";
                    break;
                case 5:
                    warningText.text = "더이상 사용할 수 었습니다.";
                    break;
                case 6:
                    warningText.text = "체력이 가득찼습니다.";
                    break;
            }
            warningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            warningText.gameObject.SetActive(false);
        }
    }
}
