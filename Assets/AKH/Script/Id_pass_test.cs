using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Id_pass_test : MonoBehaviour
{
    public InputField NicknameInput;    //유저 닉
    public InputField PasswordInput;    //유저 password
    // Start is called before the first frame update
    void Start()
    {
        //테스트 id,pass
        NicknameInput.text = "moo@naver.com";
        PasswordInput.text = "123456789zz";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
