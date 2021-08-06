using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoManager : MonoBehaviour
{
    public PlayerState playerState;
    [SerializeField] InventoryManager inventory;

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
        public int SumAngle;
        public int MoveSpeed;
        public int MissPer;
        public int BuyPrice;
        public int SellPrice;
    }

    public List<Items> ItemInfos;

    public void GetItem(int ItemCode)
    {
        Debug.Log("먹은 아이템 :" + ItemInfos[ItemCode].Name);

        if (playerState.attackSpeedPer + ItemInfos[ItemCode].AttackSpeed < 50)
            playerState.attackSpeedPer = 50;
        else if (playerState.attackSpeedPer + ItemInfos[ItemCode].AttackSpeed > 300)
            playerState.attackSpeedPer = 300;
        else
            playerState.attackSpeedPer += ItemInfos[ItemCode].AttackSpeed;

        if (playerState.moveSpeedPer + ItemInfos[ItemCode].MoveSpeed < 100)
            playerState.moveSpeedPer = 100;
        else if (playerState.moveSpeedPer + ItemInfos[ItemCode].MoveSpeed > 200)
            playerState.moveSpeedPer = 200;
        else
            playerState.moveSpeedPer += ItemInfos[ItemCode].MoveSpeed;

        if (playerState.bulletAmount + ItemInfos[ItemCode].BulletAmount < 1)
            playerState.bulletAmount = 1;
        else
            playerState.bulletAmount += ItemInfos[ItemCode].BulletAmount;

        if (playerState.dmg + ItemInfos[ItemCode].Dmg < 1)
            playerState.dmg = 1;
        else if (playerState.dmg + ItemInfos[ItemCode].Dmg > 10)
            playerState.dmg = 10;
        else
            playerState.dmg += ItemInfos[ItemCode].Dmg;

        if (playerState.missPer + ItemInfos[ItemCode].MissPer < 0)
            playerState.missPer = 0;
        else if (playerState.missPer + ItemInfos[ItemCode].MissPer > 10)
            playerState.missPer = 10;
        else
            playerState.missPer += ItemInfos[ItemCode].MissPer;
    }

    public void SellItem(int ItemCode)
    {
        playerState.gold += ItemInfos[ItemCode].SellPrice;
        inventory.goldAmountText.text = playerState.gold.ToString() + "G";
    }
}
