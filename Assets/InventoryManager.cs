using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PlayerState playerState;

    [Header("Ui")]
    public Text goldAmountText;

    private void Start()
    {
        goldAmountText.text = playerState.gold.ToString() + "G";
    }
}
