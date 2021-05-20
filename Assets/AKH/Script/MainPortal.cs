using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainPortal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Main_map 변경");
        PhotonNetwork.LoadLevel("Main_map");
        Playerspawn.MainSceneBack = true;
    }
}
