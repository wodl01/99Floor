using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBoxScript : MonoBehaviour
{
    [Header("Manager")]
    public StageManager stageManager;
    InteractManager interactManager;
    public ItemInfoManager itemInfoManager;
    [SerializeField] PlayerState playerState;

    [Header("BoxState")]
    [SerializeField] bool devil;
    [SerializeField] bool holy;
    [SerializeField] bool needKey;

    [Header("BoxShape")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    [SerializeField] Sprite touchSprite;


    [Header("BoxInteract")]
    [SerializeField] bool canInteract;
    [SerializeField] bool isOpen;
    [SerializeField] bool itemOut;
    [SerializeField] bool itemExist;
    [SerializeField] GameObject boxUiOb;
    [SerializeField] GameObject buttonIconObject;
    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] GameObject itemSellPanel;

    [Header("ItemOB")]
    [SerializeField] GameObject itemObject;
    [SerializeField] SpriteRenderer itemSpriteRenderer;

    [Header("ItemPercentage")]
    [SerializeField] int normalPer;
    [SerializeField] int rarePer;
    [SerializeField] int epicPer;
    [SerializeField] int legendPer;
    [SerializeField] Color normalColor;
    [SerializeField] Color rareColor;
    [SerializeField] Color epicColor;
    [SerializeField] Color legendColor;
    [SerializeField] Color devilColor;
    [SerializeField] Color holyColor;

    [Header("ItemCode")]
    [SerializeField] int selectedItemCode;
    [SerializeField] List<int> normalItemCodes;
    [SerializeField] List<int> rareItemCodes;
    [SerializeField] List<int> epicItemCodes;
    [SerializeField] List<int> legendItemCodes;
    [SerializeField] List<int> devilItemCodes;
    [SerializeField] List<int> holyItemCodes;

    [Header("ItemInfoPanel")]
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemGradeText;
    [SerializeField] Text itemInfoText;
    [SerializeField] Text[] itemNormalOptionTexts;
    [SerializeField] GameObject[] itemPassiveTexts;
    [SerializeField] Text itemSellPriceText;

    [Header("BoxAnimation")]
    [SerializeField] Animator boxAnimator;
    private void Start()
    {
        playerState = PlayerState.playerState;
        interactManager = InteractManager.inter;
        stageManager = StageManager.stageManager;
        itemInfoManager = ItemInfoManager.itemInfo;

        itemObject.SetActive(false);
        itemInfoPanel.SetActive(false);
        itemSellPanel.SetActive(false);
        buttonIconObject.SetActive(false);
        itemExist = true;

        for (int i = 0; i < itemInfoManager.ItemInfos.Count; i++)
        {
            switch (itemInfoManager.ItemInfos[i].Grade)
            {
                case -2:
                    devilItemCodes.Add(i);
                    break;
                case -1:
                    holyItemCodes.Add(i);
                    break;
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

        isOpen = false;
        canInteract = false;
        itemOut = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && itemExist)
        {
            canInteract = true;

            if (isOpen == false)
            {
                spriteRenderer.sprite = touchSprite;
                InteractManager.inter.SetInfo(0, gameObject, true, false);
                buttonIconObject.SetActive(true);
            }
            else if (itemOut)
            {
                ItemInfoUpdate();
                InteractManager.inter.SetInfo(1, gameObject, true, true);
                itemInfoPanel.SetActive(true);
                itemSellPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && itemExist)
        {
            canInteract = false;

            if (isOpen == false)
            {
                spriteRenderer.sprite = offSprite;
                buttonIconObject.SetActive(false);
                InteractManager.inter.SetInfo(0, gameObject, false, false);
            }
            else if (itemOut)
            {
                itemInfoPanel.SetActive(false);
                itemSellPanel.SetActive(false);
                InteractManager.inter.SetInfo(0, gameObject, false, false);
            }
        }
    }

    public void OpenBox()
    {
        if (!itemExist) return;

        if (needKey)
        {
            if (playerState.key > 0)
                InventoryManager.inventory.KeyIconUpdate(-1);

            else
            {
                StartCoroutine(interactManager.WarningText(3));
                return;
            }
        }

        if (canInteract && !isOpen)
        {
            //if (interactManager.boxCheckText.gameObject.activeSelf) return;
            if (stageManager.allKill)
            {
                isOpen = true;
                spriteRenderer.sprite = onSprite;

                buttonIconObject.SetActive(false);

                InteractManager.inter.SetInfo(0, gameObject, false, false);
                RandomItemPick();
            }
            else
                StartCoroutine(interactManager.WarningText(0));
        }
    }
    public void GetItem()
    {
        if (!itemExist) return;
        if (canInteract && itemOut)
        {
            itemInfoManager.GetItem(selectedItemCode);
            boxUiOb.SetActive(false);
            itemObject.SetActive(false);
            itemExist = false;
            InteractManager.inter.SetInfo(0, gameObject, false, false);
        }
    }
    public void SellItem()
    {
        if (!itemExist) return;
        if (canInteract && itemOut)
        {
            itemInfoManager.SellItem(selectedItemCode);
            boxUiOb.SetActive(false);
            itemObject.SetActive(false);
            itemExist = false;
            InteractManager.inter.SetInfo(0, gameObject, false, false);
        }
    }

    void RandomItemPick()
    {
        if(devil) selectedItemCode = devilItemCodes[Random.Range(0,devilItemCodes.Count)];
        else if (holy) selectedItemCode = holyItemCodes[Random.Range(0, holyItemCodes.Count)];
        else
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
        }

        itemSpriteRenderer.sprite = itemInfoManager.ItemInfos[selectedItemCode].ItemShape;
        itemObject.SetActive(true);
        boxAnimator.SetBool("Open", true);
    }

    void ItemCameOut()
    {
        itemOut = true;

        if (canInteract)
        {
            ItemInfoUpdate();
            InteractManager.inter.SetInfo(1, gameObject, true, true);
            itemInfoPanel.SetActive(true);
            itemSellPanel.SetActive(true);
        }
        else
        {
            itemInfoPanel.SetActive(false);
            itemSellPanel.SetActive(false);
        }
    }

    void ItemInfoUpdate()
    {
        itemNameText.text = itemInfoManager.ItemInfos[selectedItemCode].Name;
        itemInfoText.text = itemInfoManager.ItemInfos[selectedItemCode].ItemInfo;
        switch (itemInfoManager.ItemInfos[selectedItemCode].Grade)
        {
            case -2:
                itemGradeText.text = "공허";
                itemGradeText.color = devilColor;
                break;
            case -1:
                itemGradeText.text = "천공";
                itemGradeText.color = holyColor;
                break;
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

        if (itemInfoManager.ItemInfos[selectedItemCode].MaxHp != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].MaxHp > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "최대체력 " + plus + itemInfoManager.ItemInfos[selectedItemCode].MaxHp;
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].MoveSpeed != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].MoveSpeed > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "이동속도 " + plus + itemInfoManager.ItemInfos[selectedItemCode].MoveSpeed + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].AttackSpeed != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].AttackSpeed > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "공격속도 " + plus + itemInfoManager.ItemInfos[selectedItemCode].AttackSpeed + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].Dmg != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].Dmg > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "데미지 " + plus + itemInfoManager.ItemInfos[selectedItemCode].Dmg;
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }
        if (itemInfoManager.ItemInfos[selectedItemCode].BulletAmount != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].BulletAmount > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "탄환개수 " + plus + itemInfoManager.ItemInfos[selectedItemCode].BulletAmount;
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].SumAngle != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].SumAngle > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "탄퍼짐 " + plus + itemInfoManager.ItemInfos[selectedItemCode].SumAngle;
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].MissPer != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].MissPer > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "회피확률 " + plus + itemInfoManager.ItemInfos[selectedItemCode].MissPer + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].BulletSpeed != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].BulletSpeed > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "탄환속도 " + plus + itemInfoManager.ItemInfos[selectedItemCode].BulletSpeed + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].RangePer != 0)
        {
            bool input = false;
            string plus = itemInfoManager.ItemInfos[selectedItemCode].RangePer > 0 ? "+" : "";
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "탄환거리 " + plus + itemInfoManager.ItemInfos[selectedItemCode].RangePer + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;
                    if (plus == "+")
                        itemNormalOptionTexts[i].color = Color.green;
                    else
                        itemNormalOptionTexts[i].color = Color.red;
                }
            }
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].PassiveNum.Length != 0)
        {
            for (int i = 0; i < itemInfoManager.ItemInfos[selectedItemCode].PassiveNum.Length; i++)
                itemPassiveTexts[itemInfoManager.ItemInfos[selectedItemCode].PassiveNum[i] - 1].SetActive(true);
        }

        itemSellPriceText.text = "+" + itemInfoManager.ItemInfos[selectedItemCode].SellPrice.ToString() + "G";
    }
}
