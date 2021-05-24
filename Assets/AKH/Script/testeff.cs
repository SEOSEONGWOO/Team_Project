using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class testeff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Main_map 변경");
            PhotonNetwork.LoadLevel("Main_map");
            Playerspawn.MainSceneBack = true;
        }
    }
}
