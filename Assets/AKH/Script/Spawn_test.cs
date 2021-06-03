using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Spawn_test : MonoBehaviourPun
{
    public static bool MainSceneBack = false;    //씬>>메인
    public static bool MainScene_2 = false;    //메인>>메인2
    public static bool MainScene_3 = false;    //메인>>메인3(트랩)
    public static bool Main = false;    //메인
    public static bool Main_2 = false;    //메인2
    public static bool Main_3 = false;    //메인3(트랩)

    public static bool count = true;  //한번만 실행시키기 위한 변수

    private float time = 2f;

    private GameObject Spawnobj;

    private Vector3 V3;
    void Start()
    {
        if (photonView.IsMine)
        {
            Spawnobj = GameObject.Find("Spawnobj");
            V3 = Spawnobj.transform.position;
            gameObject.transform.position = V3;
            //gameObject.transform.position = new Vector3(-186, 3, 354);
            Main = true;
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            //메인씬으로 이동
            if (MainSceneBack == true)
            {
                Main = true;
                Main_2 = false;
                Main_3 = false;
                PhotonNetwork.LoadLevel("MainGame_1");
                Invoke("MainSceneSpawn", time);
                MainSceneBack = false;
            }
            //메인씬2로 이동
            else if (MainScene_2 == true)
            {
                Debug.Log("Main_map 2 변경");
                Main = false;
                Main_2 = true;
                Main_3 = false;
                PhotonNetwork.LoadLevel("MainGame_2");
                Invoke("MainScene_2_Spawn", time);
                MainScene_2 = false;
            }
            //메인씬3(트랩)로 이동
            else if (MainScene_3 == true)
            {
                Debug.Log("trapmap2 변경");
                Main = false;
                Main_2 = false;
                Main_3 = true;
                PhotonNetwork.LoadLevel("MainGame_3");
                Invoke("MainScene_3_Spawn", time);
                MainScene_3 = false;
            }

            //죽었을때 처음위치로 이동
            if (Player_Scr.dead && Main && count)
            {
                Invoke("MainSceneSpawn", time);
                count = false;
            }
            else if (Player_Scr.dead && Main_2 && count)
            {
                Invoke("MainScene_2_Spawn", time);
                count = false;
            }
            else if (Player_Scr.dead && Main_3 && count)
            {
                Invoke("MainScene_3_Spawn", time);
                count = false;
            }
        }
    }
    void MainSceneSpawn()
    {
        GameM.gameStart = true;
        Spawnobj = GameObject.Find("Spawnobj");
        V3 = Spawnobj.transform.position;
        gameObject.transform.position = V3;
        //gameObject.transform.position = new Vector3(-185f, 2.7f, 255f);
    }
    void MainScene_2_Spawn()
    {
        GameM.gameStart = true;
        Spawnobj = GameObject.Find("Spawnobj");
        V3 = Spawnobj.transform.position;
        gameObject.transform.position = V3;
        //gameObject.transform.position = new Vector3(263.96f, -16f, 83.85f);
    }
    void MainScene_3_Spawn()
    {
        GameM.gameStart = true;
        Spawnobj = GameObject.Find("Spawnobj");
        V3 = Spawnobj.transform.position;
        gameObject.transform.position = V3;
        //gameObject.transform.position = new Vector3(1.7f, 80, -101);
    }
}
