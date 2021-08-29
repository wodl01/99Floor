using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintWhiteScript : MonoBehaviour
{
    Shader white;
    Shader original;

    private void Awake()
    {
        white = Shader.Find("PaintWhite");
        original = Shader.Find("Sprites-Default");
    }

    public void ChangeToWhite(SpriteRenderer sprite)
    {
        sprite.material.shader = white;
    }
    public void ChangeToOriginal(SpriteRenderer sprite)
    {
        sprite.material.shader = original;
    }
}
