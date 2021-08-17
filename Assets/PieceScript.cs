﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    SpriteRenderer sprite;

    [SerializeField] float brokeWaitTime;
    [SerializeField] float fadeSpeed;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        brokeWaitTime -= Time.deltaTime;
        if(brokeWaitTime <= 0)
        {
            Color color = sprite.color;
            color.a = Mathf.Lerp(color.a, 0, fadeSpeed * Time.deltaTime);
            sprite.color = color;
            if (color.a == 0) Destroy(gameObject);
        }
    }
}
