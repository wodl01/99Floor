using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretTile : MonoBehaviour
{
    [SerializeField] float distanceToReveal = 0.1f;

    const float fadeSpeed = 10f;

    Tilemap tilemap;
    TilemapCollider2D tilemapCollider2D;
    TilemapRenderer tilemapRenderer;
    [SerializeField] GameObject player;

    [SerializeField] bool isTriggered;
    [SerializeField] bool isRevealing;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapCollider2D = GetComponent<TilemapCollider2D>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    void Update()
    {
        if (isTriggered && !isRevealing )
        {
            isRevealing = true;
        }
        else if (!isTriggered && isRevealing)
        {
            isRevealing = false;
        }

        Color color = tilemap.color;

        color.a = Mathf.Lerp(color.a, isRevealing ? 0.3f : 1, fadeSpeed * Time.deltaTime);
        tilemap.color = color;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }

    float DistanceToBounds()
    {
        if (tilemapCollider2D == null)
        {
            return distanceToReveal;
        }
        return Vector2.Distance(tilemapCollider2D.bounds.ClosestPoint(player.transform.position), player.transform.position);
    }
}