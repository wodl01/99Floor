using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] ItemInfoManager itemInfo;
    InteractManager interactManager;
    [SerializeField] PlayerState playerState;

    [Header("ItemInfo")]
    [SerializeField] GameObject itemInfoPanel;

    [SerializeField] int selectedItemCode;
    [SerializeField] List<int> normalItemCodes;
    [SerializeField] List<int> rareItemCodes;
    [SerializeField] List<int> epicItemCodes;
    [SerializeField] List<int> legendItemCodes;

    [SerializeField] int normalPer;
    [SerializeField] int rarePer;
    [SerializeField] int epicPer;
    [SerializeField] int legendPer;
    [SerializeField] Color normalColor;
    [SerializeField] Color rareColor;
    [SerializeField] Color epicColor;
    [SerializeField] Color legendColor;

    [Header("CurState")]
    [SerializeField] bool canInteract;
    [SerializeField] bool isSoldOut;

    [Header("ItemInfoPanel")]
    [SerializeField] SpriteRenderer itemSpriteRenderer;
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemGradeText;
    [SerializeField] Text itemInfoText;
    [SerializeField] Text[] itemNormalOptionTexts;
    [SerializeField] GameObject[] itemPassiveTexts;
    [SerializeField] Text itemSellPriceText;

    private void OnEnable()
    {
        itemInfoPanel.SetActive(false);
        for (int i = 0; i < itemInfo.ItemInfos.Count; i++)
        {
            switch (itemInfo.ItemInfos[i].Grade)
            {
                case 0:
                    legendItemCodes.Add(i);
                    break;
                case 1:
                    epicItemCodes.Add(i); 
                    break;
                case 2:
                    rareItemCodes.Add(i);
                    break;
                case 3:
                    normalItemCodes.Add(i);
                    break;
            }
        }
        RandomItemPick();
    }

    void RandomItemPick()
    {
        int randomRate = Random.Range(0, 101);
        if (0 <= randomRate && randomRate < normalPer)
        {
            int randomItemNum = Random.Range(0, normalItemCodes.Count);
            selectedItemCode = normalItemCodes[randomItemNum];
            Debug.Log("일반");
        }
        else if (normalPer <= randomRate && randomRate < normalPer + rarePer)
        {
            int randomItemNum = Random.Range(0, rareItemCodes.Count);
            selectedItemCode = rareItemCodes[randomItemNum];
            Debug.Log("레어");
        }
        else if (normalPer + rarePer <= randomRate && randomRate < normalPer + rarePer + epicPer)
        {
            int randomItemNum = Random.Range(0, epicItemCodes.Count);
            selectedItemCode = epicItemCodes[randomItemNum];
            Debug.Log("서사");
        }
        else if (normalPer + rarePer + epicPer <= randomRate && randomRate < normalPer + rarePer + epicPer + legendPer)
        {
            int randomItemNum = Random.Range(0, legendItemCodes.Count);
            selectedItemCode = legendItemCodes[randomItemNum];
            Debug.Log("레전");
        }

        itemSpriteRenderer.sprite = itemInfo.ItemInfos[selectedItemCode].ItemShape;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isSoldOut)
        {
            canInteract = true;
            itemInfoPanel.SetActive(true);
            InteractManager.inter.SetInfo(5, gameObject, true, false);
            ItemInfoUpdate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isSoldOut)
        {
            canInteract = false;
            InteractManager.inter.SetInfo(0, gameObject, false, false);
            itemInfoPanel.SetActive(false);
        }
    }


    public void BuyItem()
    {
        if (canInteract && !isSoldOut)
        {
            if (playerState.gold >= itemInfo.ItemInfos[selectedItemCode].BuyPrice)
            {
                playerState.gold -= itemInfo.ItemInfos[selectedItemCode].BuyPrice;
                itemInfo.GetItem(selectedItemCode);
                itemInfoPanel.SetActive(false);
                isSoldOut = true;
            }

            else
                interactManager.WarningText(2);
        }
    }

    void ItemInfoUpdate()
    {
        itemNameText.text = itemInfo.ItemInfos[selectedItemCode].Name;
        itemInfoText.text = itemInfo.ItemInfos[selectedItemCode].ItemInfo;
        switch (itemInfo.ItemInfos[selectedItemCode].Grade)
        {
            case 0:
                itemGradeText.text = "전설";
                itemGradeText.color = legendColor;
                break;
            case 1:
                itemGradeText.text = "서사";
                itemGradeText.color = epicColor;
                break;
            case 2:
                itemGradeText.text = "희귀";
                itemGradeText.color = rareColor;
                break;
            case 3:
                itemGradeText.text = "일반";
                itemGradeText.color = normalColor;
                break;
        }

        for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            itemNormalOptionTexts[i].gameObject.SetActive(false);
        for (int i = 0; i < itemPassiveTexts.Length; i++)
            itemPassiveTexts[i].SetActive(false);

        if (itemInfo.ItemInfos[selectedItemCode].MoveSpeed != 0)
        {
            bool input = false;
            string plus = itemInfo.ItemInfos[selectedItemCode].MoveSpeed > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "이동속도 " + plus + itemInfo.ItemInfos[selectedItemCode].MoveSpeed + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }

        if (itemInfo.ItemInfos[selectedItemCode].AttackSpeed != 0)
        {
            bool input = false;
            string plus = itemInfo.ItemInfos[selectedItemCode].AttackSpeed > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "공격속도 " + plus + itemInfo.ItemInfos[selectedItemCode].AttackSpeed + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }

        if (itemInfo.ItemInfos[selectedItemCode].Dmg != 0)
        {
            bool input = false;
            string plus = itemInfo.ItemInfos[selectedItemCode].Dmg > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "데미지 " + plus + itemInfo.ItemInfos[selectedItemCode].Dmg;
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }
        if (itemInfo.ItemInfos[selectedItemCode].BulletAmount != 0)
        {
            bool input = false;
            string plus = itemInfo.ItemInfos[selectedItemCode].BulletAmount > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "탄환개수 " + plus + itemInfo.ItemInfos[selectedItemCode].BulletAmount;
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }

        if (itemInfo.ItemInfos[selectedItemCode].SumAngle != 0)
        {
            bool input = false;
            string plus = itemInfo.ItemInfos[selectedItemCode].SumAngle > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "탄퍼짐 " + plus + itemInfo.ItemInfos[selectedItemCode].SumAngle;
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }

        if (itemInfo.ItemInfos[selectedItemCode].MissPer != 0)
        {
            bool input = false;
            string plus = itemInfo.ItemInfos[selectedItemCode].MissPer > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "회피확률 " + plus + itemInfo.ItemInfos[selectedItemCode].MissPer + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }

        if (itemInfo.ItemInfos[selectedItemCode].BulletSpeed != 0)
        {
            bool input = false;
            string plus = itemInfo.ItemInfos[selectedItemCode].BulletSpeed > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "탄환속도 " + plus + itemInfo.ItemInfos[selectedItemCode].BulletSpeed + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }

        if (itemInfo.ItemInfos[selectedItemCode].RangePer != 0)
        {
            bool input = false;
            string plus = itemInfo.ItemInfos[selectedItemCode].RangePer > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "탄환거리 " + plus + itemInfo.ItemInfos[selectedItemCode].RangePer + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }

        if (itemInfo.ItemInfos[selectedItemCode].PassiveNum.Length != 0)
        {
            for (int i = 0; i < itemInfo.ItemInfos[selectedItemCode].PassiveNum.Length; i++)
                itemPassiveTexts[itemInfo.ItemInfos[selectedItemCode].PassiveNum[i] - 1].SetActive(true);
        }


        itemSellPriceText.text = "구매:-" + itemInfo.ItemInfos[selectedItemCode].BuyPrice.ToString() + "G";
        itemSellPriceText.color = playerState.gold >= itemInfo.ItemInfos[selectedItemCode].BuyPrice ? Color.green : Color.red;
    }
}
