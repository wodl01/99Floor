using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    [SerializeField] ParticleSystem[] particle;
    [SerializeField] SpriteRenderer[] sprite;

    bool once;

    private void OnEnable()
    {
        once = true;
        //for (int i = 0; i < particle.Length; i++)
        //{
        //    particle[i].Pause();
        //}
        for (int i = 0; i < sprite.Length; i++)
            sprite[i].enabled = true;
    }

    private void Update()
    {
        if(once)
        transform.position = target.position;
        if (!target.gameObject.activeSelf && once)
        {
            once = false;

            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        for (int i = 0; i < particle.Length; i++)
            particle[i].Stop();
        for (int i = 0; i < sprite.Length; i++)
            sprite[i].enabled = false;

        yield return new WaitForSeconds(1);
        gameObject.transform.position = new Vector2(100, 100);
        gameObject.SetActive(false);
    }
}
