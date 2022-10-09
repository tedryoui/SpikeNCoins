using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CoinsAmountViewController
{
    public CoinsAmountView view;

    public void UpdateAmount(int amount)
    {
        view.amount.text = "Coins: " + amount;
    }
}
