using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Main_map 2 변경");
        PhotonNetwork.LoadLevel("Main_map 2");
        Playerspawn.MainScene_2 = true;
        Playerspawn.Main = false;
        Playerspawn.Main_2 = true;
        Playerspawn.Main_3 = false;
    }
}
