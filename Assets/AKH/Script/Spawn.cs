using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviourPun 
{

    public GameObject prefab;
    void Start()
    {
        PhotonNetwork.Instantiate(prefab.name, 
            new Vector3(1.773168f, 0.05f, 0.215986f), Quaternion.identity);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 룸을 나가면 로비 씬으로 돌아감
            SceneManager.LoadScene("Login");
        }*/
    }
    
}
