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

    [SerializeField] GameObject interactionOb;
    bool canInteract;
    bool canUse;

    [SerializeField] GameObject itemOutPos;

    private void Start()
    {
        canUse = true;
        interactionOb.SetActive(false);
        pool = PoolManager.pool;
        playerState = PlayerState.playerState;
        stageManager = StageManager.stageManager;
        inventory = InventoryManager.inventory;
        interactManager = InteractManager.inter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canUse) return;
        canInteract = true;
        interactionOb.SetActive(true);
        interactManager.SetInfo(7, gameObject, true, false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!canUse) return;
        canInteract = false;
        interactionOb.SetActive(false);
        interactManager.SetInfo(7, gameObject, false, false);
    }

    public void UseRiver()
    {
        if (canUse)
        {
            int randomNum = Random.Range(0, 90);
            if (randomNum >= 0 && randomNum < 30)
            {
                for (int i = 0; i < 25; i++)
                {
                    GameObject gold = pool.PoolInstantiate("Gold_Item", transform.position, Quaternion.identity);
                    stageManager.poolDestroyList.Add(gold);
                    gold.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)));
                }
            }
            else if (randomNum >= 30 && randomNum < 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    GameObject gold = pool.PoolInstantiate("Gold_Item", transform.position, Quaternion.identity);
                    stageManager.poolDestroyList.Add(gold);
                    gold.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)));
                }
            }
            else
            {
                playerState.player.GetComponent<PlayerControlScript>().hitboxScript.PlayerHit(playerState.life - 1);
                canUse = false;
            }
        }
        else
            StartCoroutine(interactManager.WarningText(5));
    }
}
