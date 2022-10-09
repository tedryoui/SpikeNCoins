using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour
{
    public CoinsAmountViewController coinsViewController;
    public EndScreenViewController endScreenViewController;

    public void HideEndScreen() => endScreenViewController.Hide();
}
