using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public Sprite[] effectSprites;
    public float spriteCycleTime;

    int curSpriteNum;
    float curCycleTime;

    private void OnEnable()
    {

        curSpriteNum = 0;
        curCycleTime = spriteCycleTime;
    }

    public void Update()
    {
        if(curCycleTime > 0)
        curCycleTime -= Time.deltaTime;

        if(curCycleTime <= 0)
        {
            curCycleTime = spriteCycleTime;

            spriteRenderer.sprite = effectSprites[curSpriteNum];
            curSpriteNum++;

            if(effectSprites[curSpriteNum] == null)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
