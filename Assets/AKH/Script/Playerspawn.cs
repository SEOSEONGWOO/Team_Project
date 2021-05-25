using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Playerspawn : MonoBehaviour
{
    public static bool MainSceneBack = false;    //씬>>메인
    public static bool MainScene_2 = false;    //메인>>메인2
    public static bool MainScene_3 = false;    //메인>>메인3(트랩)
    public static bool Main = false;    //메인
    public static bool Main_2 = false;    //메인2
    public static bool Main_3 = false;    //메인3(트랩)

    private float time = 1.5f;
    void Start()
    {
        gameObject.transform.position = new Vector3(-186, 3, 354);
        Main = true;
    }
    void Update()
    {
        //메인씬으로 이동
        if (MainSceneBack == true)
        {
            Main = true;
            Main_2 = false;
            Main_3 = false;
            Invoke("MainSceneSpawn", time);
            MainSceneBack = false;
        }
        //메인씬2로 이동
        else if(MainScene_2 == true)
        {
            Debug.Log("Main_map 2 변경");
            Main = false;
            Main_2 = true;
            Main_3 = false;
            PhotonNetwork.LoadLevel("Main_map 2");
            Invoke("MainScene_2_Spawn", time);
            MainScene_2 = false;
        }
        //메인씬3(트랩)로 이동
        else if(MainScene_3 == true)
        {
            Debug.Log("trapmap2 변경");
            Main = false;
            Main_2 = false;
            Main_3 = true;
            PhotonNetwork.LoadLevel("trapmap2");
            Invoke("MainScene_3_Spawn", time);
            MainScene_3 = false;
        }

        //죽었을때 처음위치로 이동
        if (Player_Scr.dead && Main)
        {
            Invoke("MainSceneSpawn", time);
        }
        else if (Player_Scr.dead && Main_2)
        {
            Invoke("MainScene_2_Spawn", time);
        }
        else if (Player_Scr.dead && Main_3)
        {
            Invoke("MainScene_3_Spawn", time);
        }
    }
    void MainSceneSpawn()
    {
        gameObject.transform.position = new Vector3(-185f, 2.7f, 255f);
    }
    void MainScene_2_Spawn()
    {
        gameObject.transform.position = new Vector3(263.96f, -16f, 83.85f);
    }
    void MainScene_3_Spawn()
    {
        gameObject.transform.position = new Vector3(1.7f, 80, -101);
    }
}
