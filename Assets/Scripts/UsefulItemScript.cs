using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulItemScript : MonoBehaviour
{
    InventoryManager inventory;

    PlayerState playerState;
    [SerializeField] int itemNum;
    [SerializeField] int plusNum;

    private void Start()
    {
        playerState = PlayerState.playerState;
        inventory = InventoryManager.inventory;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch (itemNum)
            {
                case 0:
                    if(playerState.key != 3)
                    {
                        inventory.KeyIconUpdate(plusNum);
                        Destroy(gameObject);
                    }
                    break;
                case 1:
                    if (playerState.bomb != 3)
                    {
                        inventory.BombIconUpdate(plusNum);
                        Destroy(gameObject);
                    }
                    break;
                case 2:
                    inventory.GoldAmountUpdate(plusNum);
                    Destroy(gameObject);
                    break;
            }
        }

    }
}
