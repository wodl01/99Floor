using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    [SerializeField] Vector2 size;


    public void SummonExplosion()
    {
        GameObject Explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Explosion.transform.localScale = size;
        Destroy(gameObject);
    }
}
