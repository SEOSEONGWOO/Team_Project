﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Main_2_Change : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Main_map 2 변경");
        PhotonNetwork.LoadLevel("Main_map 2");
        Playerspawn.MainScene_2 = true;
    }
}
