using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyPadScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] PlayerControlScript player;
    [SerializeField] PlayerState playerState;

    [SerializeField] private bool isInput;

    [SerializeField] RectTransform lever;
    [SerializeField] RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector2 inputDirection;

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoySticklever(eventData);
        isInput = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoySticklever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;

        isInput = false;
    }

    private void ControlJoySticklever(PointerEventData eventData)
    {
        var inputpos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputpos.magnitude < leverRange ? inputpos : inputpos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }

    private void Update()
    {
        if (isInput)
            MoveCharacter(inputDirection);
        else
            player.rigid.velocity = new Vector2(0, 0);
    }

    void MoveCharacter(Vector2 direction)
    {
        player.rigid.velocity = direction * Time.deltaTime * playerState.moveSpeed * (playerState.moveSpeedPer / 100);
    }
}

