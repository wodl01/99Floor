using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyGroup;

    public float destroyTime;

    [SerializeField] float holdCycleCoolTime;
    [SerializeField] int holdForce;

    [SerializeField] Animator ani;
    private void OnEnable()
    {
        StartCoroutine(Hold());
        ani.SetBool("End", false);
    }

    IEnumerator Hold()
    {
        for (int i = 0; i < enemyGroup.Count; i++)
        {
            float mainAngle = Mathf.Atan2(enemyGroup[i].transform.position.y - gameObject.transform.position.y, enemyGroup[i].transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg;


            Vector3 force = Quaternion.AngleAxis(mainAngle, Vector3.forward) * Vector3.right;
            enemyGroup[i].GetComponent<Rigidbody2D>().AddForce(-force * holdForce);
        }
        yield return new WaitForSeconds(holdCycleCoolTime);
        StartCoroutine(Hold());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Piece") enemyGroup.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Piece") enemyGroup.Remove(collision.gameObject);
    }

    private void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0) ani.SetBool("End", true);
    }
}
