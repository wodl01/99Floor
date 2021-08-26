using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryRibber : MonoBehaviour
{
    PlayerState playerState;
    PoolManager pool;
    StageManager stageManager;
    InventoryManager inventory;
    InteractManager interactManager;

    [Header("Goods")]
    [SerializeField] GameObject goldOb;

    [SerializeField] GameObject interactionOb;
    bool canInteract;

    [SerializeField] GameObject itemOutPos;

    private void Start()
    {
        interactionOb.SetActive(false);
        pool = PoolManager.pool;
        playerState = PlayerState.playerState;
        stageManager = StageManager.stageManager;
        inventory = InventoryManager.inventory;
        interactManager = InteractManager.inter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canInteract = true;
            interactionOb.SetActive(true);
            interactManager.SetInfo(7, gameObject, true, false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
            interactionOb.SetActive(false);
            interactManager.SetInfo(7, gameObject, false, false);
        }
    }

    public void UseRiver()
    {
        if (playerState.life != 1)
        {
            int randomNum = Random.Range(0, 90);
            if (randomNum >= 0 && randomNum < 30)
            {
                for (int i = 0; i < 25; i++)
                {
                    SpawnGold();
                }
            }
            else if (randomNum >= 30 && randomNum < 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    SpawnGold();
                }
            }
            else
            {
                playerState.player.GetComponent<PlayerControlScript>().hitboxScript.PlayerHit(playerState.life - 1);

            }
        }
        else
            StartCoroutine(interactManager.WarningText(5));
    }

    public void SpawnGold()
    {
        GameObject gold = pool.PoolInstantiate(goldOb, itemOutPos.transform, Quaternion.identity);
        gold.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-400, 400), Random.Range(0, -400)));
    }
}
