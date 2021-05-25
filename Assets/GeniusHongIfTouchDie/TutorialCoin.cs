using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCoin : MonoBehaviour
{
    public GameObject NextObject;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player_Scr.PlayerMoney = Player_Scr.PlayerMoney + 500;
            NextObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
