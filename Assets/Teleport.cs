using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] float teleportCool;
    public float curTeleportCool;

    [SerializeField] GameObject teleportSpot;
    [SerializeField] bool canInteract;
    [SerializeField] GameObject interactUi;
    GameObject player;

    private void Start()
    {
        player = PlayerState.playerState.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
            interactUi.SetActive(true);
            InteractManager.inter.SetInfo(2, gameObject, true, false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canInteract = false;
            interactUi.SetActive(false);
            InteractManager.inter.SetInfo(2, gameObject, false, false);
        }
    }

    private void Update()
    {
        if (curTeleportCool >= 0)
            curTeleportCool -= Time.deltaTime;
    }

    public void Tel()
    {
        if (curTeleportCool < 0 && canInteract)
        {
            curTeleportCool = teleportCool;

            player.transform.position = teleportSpot.transform.position;
            teleportSpot.GetComponent<Teleport>().curTeleportCool = teleportCool;
        }
    }
}
