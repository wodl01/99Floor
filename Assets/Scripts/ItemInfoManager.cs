using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoManager : MonoBehaviour
{
    public PlayerState playerState;
    [SerializeField] InventoryManager inventory;
    [SerializeField] ItemPassiveManager passiveManager;

    [System.Serializable]
    public class Items
    {
        public string Name;
        public int Grade;
        public string ItemInfo;
        public Sprite ItemShape;
        public int AttackSpeed;
        public int Dmg;
        public int BulletAmount;
        public int BulletSpeed;
        public int SumAngle;
        public int MoveSpeed;
        public int MissPer;
        public int RangePer;
        public int[] PassiveNum;
        public int BuyPrice;
        public int SellPrice;
    }

    public List<Items> ItemInfos;

    public void GetItem(int ItemCode)
    {
        Debug.Log("먹은 아이템 :" + ItemInfos[ItemCode].Name);

        if (playerState.attackSpeedPer + ItemInfos[ItemCode].AttackSpeed < 50)           //공속
            playerState.attackSpeedPer = 50;
        else if (playerState.attackSpeedPer + ItemInfos[ItemCode].AttackSpeed > 300)
            playerState.attackSpeedPer = 300;
        else
            playerState.attackSpeedPer += ItemInfos[ItemCode].AttackSpeed;



        if (playerState.moveSpeedPer + ItemInfos[ItemCode].MoveSpeed < 100)              //이속
            playerState.moveSpeedPer = 100;
        else if (playerState.moveSpeedPer + ItemInfos[ItemCode].MoveSpeed > 200)
            playerState.moveSpeedPer = 200;
        else
            playerState.moveSpeedPer += ItemInfos[ItemCode].MoveSpeed;



        if (playerState.bulletAmount + ItemInfos[ItemCode].BulletAmount < 1)             //총알 개수
            playerState.bulletAmount = 1;
        else
            playerState.bulletAmount += ItemInfos[ItemCode].BulletAmount;



        if (playerState.bulletSpeedPer + ItemInfos[ItemCode].BulletSpeed < 100)       //총알 속도
            playerState.bulletSpeedPer = 100;
        else if (playerState.bulletSpeedPer + ItemInfos[ItemCode].BulletSpeed > 200)
            playerState.bulletSpeedPer = 200;
        else
            playerState.bulletSpeedPer += ItemInfos[ItemCode].BulletSpeed;



        if (playerState.dmg + ItemInfos[ItemCode].Dmg < 1)                               //데미지
            playerState.dmg = 1;
        else if (playerState.dmg + ItemInfos[ItemCode].Dmg > 20)
            playerState.dmg = 20;
        else
            playerState.dmg += ItemInfos[ItemCode].Dmg;



        if (playerState.missPer + ItemInfos[ItemCode].MissPer < 0)                       //회피확률
            playerState.missPer = 0;
        else if (playerState.missPer + ItemInfos[ItemCode].MissPer > 10)
            playerState.missPer = 10;
        else
            playerState.missPer += ItemInfos[ItemCode].MissPer;



        if (playerState.bulletSumAngle + ItemInfos[ItemCode].SumAngle < 10)                     //탄퍼짐
            playerState.bulletSumAngle = 10;
        else if (playerState.missPer + ItemInfos[ItemCode].SumAngle > 360)
            playerState.bulletSumAngle = 360;
        else
            playerState.bulletSumAngle += ItemInfos[ItemCode].SumAngle;



        if (playerState.bulletRangePer + ItemInfos[ItemCode].RangePer < 0)                      //사거리
            playerState.bulletRangePer = 0;
        else
            playerState.bulletRangePer += ItemInfos[ItemCode].RangePer;

        inventory.ItemListUpdate(ItemCode);


        for (int i = 0; i < ItemInfos[ItemCode].PassiveNum.Length; i++)
        {
            switch (ItemInfos[ItemCode].PassiveNum[i])
            {
                case 1:
                    passiveManager.Passive_1 = true;
                    break;
                case 2:
                    passiveManager.Passive_2 = true;
                    break;
                case 3:
                    passiveManager.Passive_3 = true;
                    break;
                case 4:
                    passiveManager.Passive_4 = true;
                    break;
                case 5:
                    passiveManager.Passive_5 = true;
                    break;
            }
        }
    }

    public void SellItem(int ItemCode)
    {
        playerState.gold += ItemInfos[ItemCode].SellPrice;
        inventory.goldAmountText.text = playerState.gold.ToString() + "G";
    }
}
