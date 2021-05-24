using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Main_3_Change : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Main_map_3 변경");
        //PhotonNetwork.LoadLevel("Main_map");
        //Playerspawn.MainSceneBack = true;
    }
}
