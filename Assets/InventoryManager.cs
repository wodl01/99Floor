using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager inventory;

    [Header("Scripts")]
    [SerializeField] PlayerState playerState;
    [SerializeField] ItemInfoManager itemInfo;
    [SerializeField] InteractManager interact;

    [Header("Ui")]
    public Text goldAmountText;
    [SerializeField] GameObject[] keyIcons;
    [SerializeField] GameObject[] bombIcons;

    [Header("ItemSlot")]
    [SerializeField] Image[] itemSlotImages;
    [SerializeField] List<int> itemNum = new List<int>();

    private void Awake()
    {
        inventory = this;
        goldAmountText.text = playerState.gold.ToString() + "G";
        KeyIconUpdate();
        BombIconUpdate();
    }

    public void KeyIconUpdate()
    {
        for (int i = 0; i < keyIcons.Length; i++)
            keyIcons[i].SetActive(false);

        for (int i = 0; i < playerState.key; i++)
            keyIcons[i].SetActive(true);
    }

    public void BombIconUpdate()
    {
        for (int i = 0; i < bombIcons.Length; i++)
            bombIcons[i].SetActive(false);

        for (int i = 0; i < playerState.bomb; i++)
            bombIcons[i].SetActive(true);

        interact.buttonC.SetActive(playerState.bomb > 0 ? true : false);
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
