using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackJoyPad : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] PlayerAniManager playerAni;
    [SerializeField] PlayerControlScript player;
    [SerializeField] PlayerState playerState;

    public bool isInput;

    [SerializeField] RectTransform lever;
    [SerializeField] RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    [SerializeField] Vector2 inputDirection;
    public float rotation;

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoySticklever(eventData);
        isInput = true;
        playerAni.attackPadActive = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoySticklever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;

        isInput = false;
        playerAni.attackPadActive = false;
    }

    private void ControlJoySticklever(PointerEventData eventData)
    {
        var inputpos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputpos.magnitude < leverRange ? inputpos : inputpos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
        rotation = Mathf.Atan2(rectTransform.transform.position.y - lever.transform.position.y, rectTransform.transform.position.x - lever.transform.position.x) * Mathf.Rad2Deg;

        playerAni.attackRotate = rotation;
    }

    private void Update()
    {
        if (isInput)
            Attack(inputDirection);
    }

    void Attack(Vector2 direction)
    {

        player.MobileFarAttack(rotation);
    }
}
