using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Waiting : MonoBehaviour
{
    public Text playerCountTxt;
    public GameObject waitCanvas;
    public GameObject player;
    public GameObject UIBAR;
    public GameObject EnemyAI;
    public GameObject CameraHolder;
    public GameObject SubCamera;
    public GameObject CanvasAim;

    public static bool isSpawn = false;
    private void Start()
    {
        player.SetActive(false);
        UIBAR.SetActive(false);
        //EnemyAI.SetActive(false);
        CameraHolder.SetActive(false);
        CanvasAim.SetActive(false);

        SubCamera.SetActive(true);
        waitCanvas.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        playerCountTxt.text = "현재 " + PhotonNetwork.CurrentRoom.PlayerCount + "명 입니다.";
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("PlayerCount : "+ PhotonNetwork.CurrentRoom.PlayerCount);

            player.SetActive(true);
            UIBAR.SetActive(true);
            CameraHolder.SetActive(true);
            CanvasAim.SetActive(true);
            //EnemyAI.SetActive(true);

            SubCamera.SetActive(false);
            waitCanvas.SetActive(false);

            isSpawn = true;
        }
    }
}
