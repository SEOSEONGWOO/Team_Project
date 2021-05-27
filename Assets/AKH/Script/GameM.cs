using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameM : MonoBehaviour
{
    public static bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameStart = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {
        //GUILayout.Label(DB_test_net.connectionInfoText.text);
    }
}
