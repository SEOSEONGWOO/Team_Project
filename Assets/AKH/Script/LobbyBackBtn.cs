using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyBackBtn : MonoBehaviour
{
    public void LobbyBtn()
    {
        Debug.Log("로비");
        SceneManager.LoadScene("DB_Test");
    }
    public void Exit()
    {
        Debug.Log("종료");
        Application.Quit();
    }
}
