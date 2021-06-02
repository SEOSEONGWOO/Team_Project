using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Spawn_Start_test : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefabC;
    public GameObject aim;
    int i = 0;
    public Transform spawpoint;
    void Start()
    {
        if (GameM.gameStart)
        {
            DontDestroyOnLoad(gameObject);
            // 네트워크 상의 모든 클라이언트들에서 생성 실행
            // 단, 해당 게임 오브젝트의 주도권은, 생성 메서드를 직접 실행한 클라이언트에게 있음

            /*PhotonNetwork.Instantiate(aim.name,
                new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(prefabC,
                new Vector3(-59.2f, 3.0f, 25.0f), Quaternion.identity);*/

            GameObject name = PhotonNetwork.Instantiate(prefab.name,
                new Vector3(-59.2f, 3.0f, 25.0f), Quaternion.identity);
            Debug.Log(spawpoint.position);
            GameM.gameStart = false;
        }

    }

}
