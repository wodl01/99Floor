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

    [Header("Sprites")]
    [SerializeField] SpriteRenderer doorOutRenderer;
    [SerializeField] SpriteRenderer doorInRenderer1;
    [SerializeField] SpriteRenderer doorInRenderer2;
    [SerializeField] Sprite[] doorOutlineSprite;
    [SerializeField] Sprite[] doorInSprite;


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

                SoundManager.Play("Door");
            }
            else
                StartCoroutine(inter.WarningText(1));
        }
    }

    public void ChangeTransform()
    {
        if(stageManager.curStage < 31)
        {
            doorOutRenderer.sprite = doorOutlineSprite[0];
            doorInRenderer1.sprite = doorInSprite[0];
            doorInRenderer2.sprite = doorInSprite[0];
        }
        else if(30 < stageManager.curStage && stageManager.curStage < 60)
        {
            doorOutRenderer.sprite = doorOutlineSprite[1];
            doorInRenderer1.sprite = doorInSprite[1];
            doorInRenderer2.sprite = doorInSprite[1];
        }
    }
}
