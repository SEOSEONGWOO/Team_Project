using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Playerspawn : MonoBehaviour
{
    public static bool MainSceneBack = false;    //씬>>메인
    public static bool ShopBack = false;    //메인(상점)
    public static bool MainScene_2 = false;    //메인>>메인2
    public static bool MainScene_3 = false;    //메인>>메인3(트랩)
    public static bool Main = false;    //메인
    public static bool Main_2 = false;    //메인2
    public static bool Main_3 = false;    //메인3(트랩)
    public static bool isBossRoom = false;    //보스방

    public static bool count = true;  //한번만 실행시키기 위한 변수

    private float time = 0.7f;

    public static bool BossRoom_test = false;    //BossRoom_test

    private GameObject Spawnobj;
    private GameObject Shopobj;
    private Vector3 V3;
    void Start()
    {
        //gameObject.transform.position = new Vector3(-186.0f, 0.01f, 354.0f);
        Spawnobj = GameObject.Find("Spawnobj");
        Shopobj = GameObject.Find("Shopobj");
        V3 = Spawnobj.transform.position;
        gameObject.transform.position = V3;
        Main = true;
    }
    void Update()
    {
        /*//메인씬으로 이동
        if (MainSceneBack)
        {
            //PhotonNetwork.LoadLevel("Main_map");
            SceneManager.LoadScene("Main_map");
            if (Main_2 || Main_3)
            {
                Debug.Log("ShopSpawn");
                Invoke("ShopSpawn", time);
            }
            else
            {
                Debug.Log("SceneSpawn");
                Invoke("SceneSpawn", time);
            }
            Main = true;
            Main_2 = false;
            Main_3 = false;
            MainSceneBack = false;
        }
        //메인씬2로 이동
        if(MainScene_2)
        {
            Main = false;
            Main_2 = true;
            Main_3 = false;
            Debug.Log("Main_map 2 변경");
            //PhotonNetwork.LoadLevel("Main_map 2");
            SceneManager.LoadScene("Main_map 2");
            Invoke("SceneSpawn", time);
            MainScene_2 = false;
        }
        //메인씬3(트랩)로 이동
        if(MainScene_3)
        {
            Main = false;
            Main_2 = false;
            Main_3 = true;
            Debug.Log("trapmap2 변경");
            //PhotonNetwork.LoadLevel("trapmap2");
            SceneManager.LoadScene("trapmap2");
            Invoke("SceneSpawn", time);
            MainScene_3 = false;
        }
        //bossroom test 
        if (BossRoom_test)
        {
            gameObject.transform.position = new Vector3(-185f, -1.3f, 230f);
            BossRoom_test = false;
        }*/

        //메인씬으로 이동
        if (MainSceneBack)
        {
            //PhotonNetwork.LoadLevel("Main_map");
            //SceneManager.LoadScene("Main_map");
            if (Main_2 || Main_3)
            {
                Debug.Log("ShopSpawn");
                Invoke("ShopSpawn", time);
            }
            else
            {
                Spawnobj = GameObject.Find("Spawnobj");
                Debug.Log("SceneSpawn");
                Invoke("SceneSpawn", time);
            }
            Main = true;
            Main_2 = false;
            Main_3 = false;
            MainSceneBack = false;
        }
        //메인씬2로 이동
        if (MainScene_2)
        {
            Main = false;
            Main_2 = true;
            Main_3 = false;
            Debug.Log("Main_map 2 변경");
            //PhotonNetwork.LoadLevel("Main_map 2");
            //SceneManager.LoadScene("Main_map 2");
            Spawnobj = GameObject.Find("Spawnobj_main_2");
            Invoke("SceneSpawn", time);
            MainScene_2 = false;
        }
        //메인씬3(트랩)로 이동
        if (MainScene_3)
        {
            Main = false;
            Main_2 = false;
            Main_3 = true;
            Debug.Log("trapmap2 변경");
            //PhotonNetwork.LoadLevel("trapmap2");
            //SceneManager.LoadScene("trapmap2");
            Spawnobj = GameObject.Find("Spawnobj_trapmap_2");
            Invoke("SceneSpawn", time);
            MainScene_3 = false;
        }
        //죽었을때 처음위치로 이동
        if (Player_Scr.isdead && Main)  //튜토리얼
        {
            Spawnobj = GameObject.Find("Spawnobj");
            Invoke("SceneSpawn", time);
            Player_Scr.isdead = false;
        }
        else if (Player_Scr.isdead && Main_2)   //몬스터
        {
            Spawnobj = GameObject.Find("Spawnobj_main_2");
            Invoke("SceneSpawn", time);
            Player_Scr.isdead = false;
        }
        else if (Player_Scr.isdead && Main_3)   //트랩
        {
            Spawnobj = GameObject.Find("Spawnobj_trapmap_2");
            Invoke("SceneSpawn", time);
            Player_Scr.isdead = false;
        }
        //죽었을때 처음위치로 이동
        else if (Player_Scr.isdead && isBossRoom)  //보스방
        {
            Invoke("Shopobj", time);
            Player_Scr.isdead = false;
            isBossRoom = false;
        }
    }
    void SceneSpawn()
    {
        //gameObject.transform.position = new Vector3(-185f, 5f, 263f);
        V3 = Spawnobj.transform.position;
        gameObject.transform.position = V3;
    }
    void ShopSpawn()
    {
        Shopobj = GameObject.Find("Shopobj");
        V3 = Shopobj.transform.position;
        gameObject.transform.position = V3;
    }
}
