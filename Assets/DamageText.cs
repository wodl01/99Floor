using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public float deleteTime;
    Animator ani;
    [SerializeField] Text text;

    [SerializeField] Color[] textColors;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    public void DamageOn(int amount,int damageType, bool isPlus)
    {
        text.text = (isPlus ? "+" : "-") + amount.ToString();
        text.color = textColors[damageType];
    }

    IEnumerator Timer()
    {
        ani.SetBool("Del", false);
        yield return new WaitForSeconds(deleteTime);
        ani.SetBool("Del", true);
    }

    public void DestroyDmgText()
    {
        gameObject.SetActive(false);
    }
}
