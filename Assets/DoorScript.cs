using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] StageManager stageManager;

    [Header("Inspector")]
    [SerializeField] Animator ani;

    [Header("DoorInteract")]
    [SerializeField] bool canInteract;
    [SerializeField] GameObject buttonIconObject;

    public void DoorOpen(bool state)
    {
        ani.SetBool("Open", state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canInteract = true;
            buttonIconObject.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
            buttonIconObject.SetActive(false);
        }

    }

    private void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.F))
        {
            if (stageManager.boxCheckText.gameObject.activeSelf) return;
            if (stageManager.allKill)
            {
                stageManager.PickMap();
                buttonIconObject.SetActive(false);
            }

            else
                stageManager.WarningText(1);
        }
    }
}
