using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            gameObject.transform.position = new Vector3(-184.96f, 5.15f, 263.89f);
            MainSceneBack = false;
        }
        //메인씬2로 이동
        else if (MainScene_2 == true || (Player_Scr.dead && Main_2))
        {
            gameObject.transform.position = new Vector3(263.96f, -10.65f, 83.85f);
            MainScene_2 = false;
        }
        //메인씬3(트랩)로 이동
        else if(MainScene_3 == true || (Player_Scr.dead && Main_3))
        {
            gameObject.transform.position = new Vector3(1.7f, 80f, -101f);
            MainScene_3 = false;
        }

    }

}
