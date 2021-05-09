using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class DB_Test : MonoBehaviour
{
    [SerializeField] string email;
    [SerializeField] string password;

    public InputField inputTextEmail;
    public InputField inputTextPassword;
    public Text loginResult;



    FirebaseAuth auth;
    void Awake()
    {
        // 초기화
        auth = FirebaseAuth.DefaultInstance;

    }


    // 버튼이 눌리면 실행할 함수.
    public void JoinBtnOnClick()
    {
        email = inputTextEmail.text;  //입력한 아이디 저장
        password = inputTextPassword.text;

        Debug.Log("email: " + email + ", password: " + password);

        CreateUser();
    }


    void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        { //파이어베이스로 보내기
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled."); //이메일 형식이 아니면
                loginResult.text = "회원가입 실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception); //비밀번호가 짧으면
                loginResult.text = "회원가입 실패";
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",  //파이어베이스에 정보 저장
                newUser.DisplayName, newUser.UserId);

            loginResult.text = "회원가입 굿럭";
        });
    }

    public void LoginBtnOnClick()
    {
        email = inputTextEmail.text;
        password = inputTextPassword.text;

        Debug.Log("email: " + email + ", password: " + password);

        LoginUser();
    }

    void LoginUser()
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "로그인 실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                loginResult.text = "로그인 실패";
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

        });

    }
}
