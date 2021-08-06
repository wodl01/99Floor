using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] effectSprite;

    private void OnEnable()
    {
        StartCoroutine(EffectAni());
    }

    IEnumerator EffectAni()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
