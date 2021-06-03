using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("MainGame_2 변경");
        PhotonNetwork.LoadLevel("MainGame_2");
        //SSceneManager.LoadScene("Main_map 2");
        Playerspawn.MainScene_2 = true;
    }
}
