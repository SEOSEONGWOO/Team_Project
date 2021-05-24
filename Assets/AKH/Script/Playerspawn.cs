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

    void Start()
    {
        gameObject.transform.position = new Vector3(-186, 3, 354);
    }
    void Update()
    {
        //메인씬으로 이동
        if (MainSceneBack == true || (Player_Scr.dead && Main))
        {
            Invoke("MainSceneSpawn", 2f);
            MainSceneBack = false;
        }
        //메인씬2로 이동
        if (MainScene_2 == true || (Player_Scr.dead && Main_2))
        {
            Debug.Log("Main_map 2 변경");
            PhotonNetwork.LoadLevel("Main_map 2");
            Invoke("MainScene_2_Spawn", 2f);
            MainScene_2 = false;
        }
        //메인씬3(트랩)로 이동
        if (MainScene_3 == true || (Player_Scr.dead && Main_3))
        {
            Debug.Log("trapmap2 변경");
            PhotonNetwork.LoadLevel("trapmap2");
            Invoke("MainScene_3_Spawn", 2f);
            MainScene_3 = false;
        }
    }
    void MainSceneSpawn()
    {
        gameObject.transform.position = new Vector3(-185f, 2.7f, 255f);
    }
    void MainScene_2_Spawn()
    {
        gameObject.transform.position = new Vector3(263.96f, -8f, 83.85f);
    }
    void MainScene_3_Spawn()
    {
        gameObject.transform.position = new Vector3(1.7f, 80, -101);
    }
}
