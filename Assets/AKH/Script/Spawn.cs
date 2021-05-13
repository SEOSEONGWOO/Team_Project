using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject prefab;
    int i = 0;
    void Start()
    {
        // 네트워크 상의 모든 클라이언트들에서 생성 실행
        // 단, 해당 게임 오브젝트의 주도권은, 생성 메서드를 직접 실행한 클라이언트에게 있음
        GameObject name = PhotonNetwork.Instantiate(prefab.name, 
            new Vector3(1.773168f, 0.05f, 0.215986f), Quaternion.identity);
        /*name.name = "test_name" + i;
        i++;
        Debug.Log("i : "+i+"/"+ name.name);*/
    }

}
