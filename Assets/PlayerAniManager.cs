using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniManager : MonoBehaviour
{
    [SerializeField] PlayerControlScript player;
    [SerializeField] Animator playerAni;
    [SerializeField] SpriteRenderer sprite;

    public bool movePadActive;
    public bool attackPadActive;
    public float moveRotate;
    public float attackRotate;

    int x;
    int y;
    private void Update()
    {
        if (!player.canMove) return;
        if (attackPadActive)
        {
            if (Mathf.Abs(attackRotate) <= 45 && Mathf.Abs(attackRotate) > -45)
            {
                x = -1;
                y = 0;
                sprite.flipX = true;
            }
            else if (attackRotate <= -45 && attackRotate > -135)
            {
                x = 0;
                y = 1;
            }
            else if (attackRotate >= 45 && attackRotate < 135)
            {
                x = 0;
                y = -1;
            }
            else
            {
                x = 1;
                y = 0;
                sprite.flipX = false;
            }
            if(Mathf.Abs(attackRotate) > 90)
            {
                player.isLookLeft = true;
            }
            else
            {
                player.isLookLeft = false;
            }
            playerAni.SetInteger("AxisX", x);
            playerAni.SetInteger("AxisY", y);
            playerAni.SetBool("Move", true);
        }
        else if (movePadActive)
        {
            if (Mathf.Abs(moveRotate) <= 45 && Mathf.Abs(moveRotate) > -45)
            {
                x = -1;
                y = 0;
                sprite.flipX = true;
            }
            else if (moveRotate <= -45 && moveRotate > -135)
            {
                x = 0;
                y = 1;
            }
            else if (moveRotate >= 45 && moveRotate < 135)
            {
                x = 0;
                y = -1;
            }
            else
            {
                x = 1;
                y = 0;
                sprite.flipX = false;
            }
            if (Mathf.Abs(moveRotate) > 90)
            {
                player.isLookLeft = true;
            }
            else
            {
                player.isLookLeft = false;
            }
            playerAni.SetInteger("AxisX", x);
            playerAni.SetInteger("AxisY", y);
            playerAni.SetBool("Move", true);
        }
        else
            playerAni.SetBool("Move", false);
    }
}
