using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public bool isFollow;
    public bool isRevealing;
    public float revealingSpeed;
    public Vector2 followPos;
    [SerializeField] SpriteRenderer spriteRenderer;
    public Sprite[] effectSprites;
    public float spriteCycleTime;

    int curSpriteNum;
    public int maxSpriteNum;
    float curCycleTime;

    bool cycleFinish;

    private void OnEnable()
    {
        cycleFinish = false;
        curSpriteNum = 0;
        curCycleTime = spriteCycleTime;
    }

    private void OnDisable()
    {
        for (int i = 0; i < effectSprites.Length; i++)
            effectSprites[i] = null;
    }

    public void Update()
    {
        if (isFollow)
            gameObject.transform.position = followPos;

        if(curCycleTime > 0)
        curCycleTime -= Time.deltaTime;

        if(curCycleTime <= 0 && !cycleFinish)
        {
            curCycleTime = spriteCycleTime;

            spriteRenderer.sprite = effectSprites[curSpriteNum];
            curSpriteNum++;

            if(curSpriteNum > maxSpriteNum)
            {
               /* if (isRevealing) cycleFinish = true;
                else */ gameObject.SetActive(false);
            }
        }

       /* if (cycleFinish)
        {
            Color color = spriteRenderer.color;

            color.a = Mathf.Lerp(color.a, 0, revealingSpeed * Time.deltaTime);
            spriteRenderer.color = color;

            if (color.a == 0) gameObject.SetActive(false);
        }*/
    }
}
