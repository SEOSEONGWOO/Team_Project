using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static string Nick_M ;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
