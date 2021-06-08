using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public static int KeyOn = 0;

    public GameObject Boss;

    // Update is called once per frame
    void Update()
    {
        if(KeyOn == 2)
        {
            Destroy(gameObject);
            Boss.SetActive(true);
        }
    }
}
