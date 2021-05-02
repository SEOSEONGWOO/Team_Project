using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{

    public GameObject prefab;
    void Start()
    {
        PhotonNetwork.Instantiate(prefab.name, 
            new Vector3(1.773168f, 0.05f, 0.215986f), Quaternion.identity);
    }




}
