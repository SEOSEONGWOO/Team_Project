using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Text text;

    private void Update()
    {
        text.text = Player_Scr.money.ToString();
    }
}
