using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulItemScript : MonoBehaviour
{
    InventoryManager inventory;

    PlayerState playerState;
    [SerializeField] int itemNum;

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
                        playerState.key += 1;
                        inventory.KeyIconUpdate();
                        Destroy(gameObject);
                    }
                    break;
                case 1:
                    if (playerState.bomb != 3)
                    {
                        playerState.bomb += 1;
                        inventory.BombIconUpdate();
                        Destroy(gameObject);
                    }
                    break;
            }
        }

    }
}
