using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] InteractManager inter;
    [SerializeField] StageManager stageManager;

    [Header("Inspector")]
    [SerializeField] Animator ani;

    [Header("DoorInteract")]
    [SerializeField] bool canInteract;
    [SerializeField] GameObject buttonIconObject;


    private void Start()
    {
        buttonIconObject.SetActive(false);
    }
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
            InteractManager.inter.SetInfo(4, gameObject, true, false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
            buttonIconObject.SetActive(false);
            InteractManager.inter.SetInfo(0, gameObject, false, false);
        }
    }

    public void UseDoor()
    {
        if (canInteract)
        {
            if (stageManager.allKill)
            {
                StartCoroutine(stageManager.NextMapLoad());
                buttonIconObject.SetActive(false);
            }
            else
                StartCoroutine(inter.WarningText(1));
        }
    }
}
