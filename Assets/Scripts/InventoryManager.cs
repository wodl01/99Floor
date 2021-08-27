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
    }

    private void Start()
    {
        goldAmountText.text = playerState.gold.ToString() + "G";
        KeyIconUpdate(0);
        BombIconUpdate(0);
    }

    public void GoldAmountUpdate(int index)
    {
        playerState.gold += index;

        goldAmountText.text = playerState.gold.ToString() + "G";
    }

    public void KeyIconUpdate(int index)
    {
        playerState.key += index;

        for (int i = 0; i < keyIcons.Length; i++)
            keyIcons[i].SetActive(false);

        for (int i = 0; i < playerState.key; i++)
            keyIcons[i].SetActive(true);
    }

    public void BombIconUpdate(int index)
    {
        playerState.bomb += index;

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
