using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviourPun //, IPunObservable
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
    /*void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //자신의 플레이어 정보는 다른 네트워크 사용자에게 송신
            stream.SendNext(PlayerCs.tr.position);
            stream.SendNext(PlayerCs.tr.rotation);
        }
        else
        {
            //다른 플레이어의 정보는 수신
            PlayerCs.tr.position = (Vector3)stream.ReceiveNext();
            PlayerCs.tr.rotation = (Quaternion)stream.ReceiveNext();
        }
    }*/
}
