using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyPadScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] PlayerAniManager playerAni;
    [SerializeField] PlayerControlScript player;
    [SerializeField] PlayerState playerState;

    [SerializeField] private bool isInput;

    [SerializeField] RectTransform lever;
    [SerializeField] RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector2 inputDirection;
    [SerializeField] float rotation;

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoySticklever(eventData);
        isInput = true;
        playerAni.movePadActive = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoySticklever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;

        isInput = false;
        playerAni.movePadActive = false;
    }

    private void ControlJoySticklever(PointerEventData eventData)
    {
        var inputpos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputpos.magnitude < leverRange ? inputpos : inputpos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;

        rotation = Mathf.Atan2(rectTransform.transform.position.y - lever.transform.position.y, rectTransform.transform.position.x - lever.transform.position.x) * Mathf.Rad2Deg;
        playerAni.moveRotate = rotation;
    }

    private void Update()
    {
        if (player.isTest) return;
        if (isInput)
            MoveCharacter(inputDirection);
        else
            player.rigid.velocity = new Vector2(0, 0);
    }

    void MoveCharacter(Vector2 direction)
    {
        if(player.canMove)
        player.rigid.velocity = direction * Time.deltaTime * playerState.moveSpeed * (playerState.moveSpeedPer / 100);
    }
}

