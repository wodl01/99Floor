using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PlayerState playerState;
    [SerializeField] ItemInfoManager itemInfo;

    [Header("Ui")]
    public Text goldAmountText;

    [Header("ItemSlot")]
    [SerializeField] Image[] itemSlotImages;
    [SerializeField] List<int> itemNum = new List<int>();

    private void Start()
    {
        goldAmountText.text = playerState.gold.ToString() + "G";
    }

    public void ItemListUpdate(int code)
    {
        itemNum.Add(code);
        for (int i = 0; i < itemNum.Count; i++)
        {
            itemSlotImages[i].sprite = itemInfo.ItemInfos[itemNum[i]].ItemShape;
        }
    }
}
