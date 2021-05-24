using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerspawn : MonoBehaviour
{
    public static bool MainSceneBack = false;    //씬>>메인
    public static bool MainScene_2 = false;    //메인>>메인2
    public static bool MainScene_3 = false;    //메인>>메인3(트랩)

    void Start()
    {
        gameObject.transform.position = new Vector3(-186, 3, 354);
    }
    void Update()
    {
        //메인씬으로 이동
        if (MainSceneBack == true)
        {
            gameObject.transform.position = new Vector3(-184.96f, 5.15f, 263.89f);
            MainSceneBack = false;
        }
        //메인씬2로 이동
        if (MainScene_2 == true)
        {
            gameObject.transform.position = new Vector3(263.96f, -10.65f, 83.85f);
            MainScene_2 = false;
        }

        //적용 전
        //메인씬3(트랩)로 이동
        if (MainScene_3 == true)
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            MainScene_3 = false;
        }

    }

}
