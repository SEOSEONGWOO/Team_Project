using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Main_3_Change : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trapmap2 변경");
        PhotonNetwork.LoadLevel("trapmap2");
        Playerspawn.MainScene_3 = true;
        Playerspawn.Main = false;
        Playerspawn.Main_2 = false;
        Playerspawn.Main_3 = true;
    }
}
