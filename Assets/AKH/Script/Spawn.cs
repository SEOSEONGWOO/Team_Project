using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    /*public Rigidbody RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text NickNameText;
    public Image HealthImage;*/


    /*bool isGround;

    Vector3 curPos;*/

    /*void Awake()
    {
        //닉네임
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;
    }*/

    public Transform prefab;
    public void newObject()
    {
        Instantiate(prefab, new Vector3(1.773168f, 0.05f, 0.215986f), Quaternion.identity);
    }




}
