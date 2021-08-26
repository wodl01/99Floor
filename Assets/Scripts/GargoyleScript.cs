using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GargoyleScript : MonoBehaviour
{
    PlayerState playerState;
    InventoryManager inventory;
    InteractManager interactManager;

    [SerializeField] GameObject interactionPanel;
    [SerializeField] bool canInteract;
    [SerializeField] int canWishAmount;
    [SerializeField] string speech_Can;
    [SerializeField] string speech_Cant;

    [SerializeField] Transform outPos;

    [Header("Ui")]
    [SerializeField] Text leftWishText;
    [SerializeField] Text speechText;

    [Header("Need")]
    [SerializeField] int gold;

    [Header("Goods")]
    [SerializeField] GameObject keyOb;
    [SerializeField] GameObject bombOb;
    [SerializeField] int life;
    [SerializeField] int key;
    [SerializeField] int bomb;

    private void Start()
    {
        playerState = PlayerState.playerState;
        inventory = InventoryManager.inventory;
        interactManager = InteractManager.inter;

        UiUpdate();

        interactionPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canInteract = true;
            interactionPanel.SetActive(true);
            InteractManager.inter.SetInfo(6, gameObject, true, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
            interactionPanel.SetActive(false);
            InteractManager.inter.SetInfo(0, gameObject, false, false);
        }
    }

    void UiUpdate()
    {
        leftWishText.text = "기도(남은 횟수:" + canWishAmount + ")";

        if (canWishAmount > 0) speechText.text = speech_Can;
        else speechText.text = speech_Cant;
    }

    public void InteractGagoyle()
    {
        if (canInteract && canWishAmount > 0)
        {
            if (playerState.gold >= gold)
            {
                playerState.gold -= gold;
                if(life > 0)
                {
                    if (playerState.life != playerState.maxLife)
                    {
                        canWishAmount--;
                        playerState.player.GetComponent<PlayerControlScript>().hitboxScript.PlayerHeal(life);
                    }
                    else
                        StartCoroutine(interactManager.WarningText(6));
                }
                if(key > 0)
                {
                    canWishAmount--;
                    for (int i = 0; i < key; i++)
                    {
                        GameObject key = PoolManager.pool.PoolInstantiate(keyOb, outPos, Quaternion.identity);
                        key.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-400, 400), Random.Range(0, -400)));
                    }
                }
                if (bomb > 0)
                {
                    canWishAmount--;
                    for (int i = 0; i < bomb; i++)
                    {
                        GameObject bomb = PoolManager.pool.PoolInstantiate(bombOb, outPos, Quaternion.identity);
                        bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-400, 400), Random.Range(0, -400)));
                    }
                }
                UiUpdate();
                inventory.GoldAmountUpdate(0);
            }
            else
                StartCoroutine(interactManager.WarningText(4));
        }
        else
            StartCoroutine(interactManager.WarningText(5));
    }
}
