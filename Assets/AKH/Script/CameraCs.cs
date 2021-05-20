using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using Photon.Pun;
using Photon.Realtime;

public class CameraCs : MonoBehaviourPun
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
