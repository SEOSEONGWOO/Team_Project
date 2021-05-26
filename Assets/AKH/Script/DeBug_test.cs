using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DeBug_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        //화면 좌측 상단에 접속 과정에 대한 로그를 출력
        //GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}
