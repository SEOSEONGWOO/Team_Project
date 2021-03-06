using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Database;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class DB2 : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전

    [SerializeField] string email;
    [SerializeField] string password;
    private string user_id;
    

    bool SceanChange = false;
    public InputField NicknameInput;    //유저 닉
    public InputField PasswordInput;    //유저 password
    public Text Title;
    private static string nicknameText;           //nickname 저장
    private static string passowrdText;           //password 저장

    public GameObject LoginPenel;  //LoginPenel
    public GameObject LobbyPenel;  //LobbyPenel


    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button joinButton; // 룸 접속 버튼
    public Button LogButton;    //로그인 버튼
    FirebaseAuth auth;
    DatabaseReference reference;

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference; //디비 사용 선언

        //password 초기화
        passowrdText = "";
        
        // 접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;

        // 설정한 정보를 가지고 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        // 룸 접속 버튼을 잠시 비활성화
        //joinButton.interactable = false;
        // 로그인 접속 버튼을 잠시 비활성화
        LogButton.interactable = false;

        //서버상태 메세지 컴포넌트
        connectionInfoText = GameObject.Find("Connection").GetComponent<Text>();

        // 접속을 시도 중임을 텍스트로 표시
        connectionInfoText.text = "마스터 서버에 접속중...";

    }
    private void Update()
    {
        if (SceanChange == true)
        {
            Debug.Log("로그인 완료");
            // 마스터 서버에 접속중이라면
            if (PhotonNetwork.IsConnected)
            {
                // 로그인 접속 버튼을 잠시 비활성화
                LogButton.interactable = false;

                Debug.Log("룸 접속 실행");
                // 룸 접속 실행
                connectionInfoText.text = "룸에 접속...";
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // 로그인 접속 버튼을 잠시 비활성화
                LogButton.interactable = true;
                // 마스터 서버에 접속중이 아니라면, 마스터 서버에 접속 시도
                connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
                // 마스터 서버로의 재접속 시도
                PhotonNetwork.ConnectUsingSettings();
            }

            SceanChange = false;
        }
    }
    //회원가입 코드 시작
    public void JoinBtnOnClick()
    {
        email = NicknameInput.text;  //입력한 아이디 저장
        password = PasswordInput.text;

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
                Title.text = "회원가입 실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception); //비밀번호가 짧으면
                Title.text = "회원가입 실패";
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",  //파이어베이스에 정보 저장
                newUser.DisplayName, newUser.UserId);

            Title.text = "회원가입 굿럭";
        });
    }
    //회원가입 코드 끝

    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        // 접속 정보 표시
        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";

        // 룸 접속 버튼을 활성화
        //joinButton.interactable = true;
        // 로그인 접속 버튼을 활성화
        LogButton.interactable = true;
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        // 룸 접속 버튼을 비활성화
        //joinButton.interactable = false;
        // 로그인 접속 버튼을 비활성화
        LogButton.interactable = false;
        // 접속 정보 표시
        connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";

        // 마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    // 룸 접속 시도
    public void Connect()
    {
        // 중복 접속 시도를 막기 위해, 접속 버튼 잠시 비활성화
        //유저 닉 가져오기
        PhotonNetwork.LocalPlayer.NickName = NicknameInput.text;

        /*----------DB저장----------*/
        //nickname 저장
        nicknameText = NicknameInput.text;
        //password 저장
        passowrdText = PasswordInput.text;
        Debug.Log("nick : " + nicknameText + ", pass : " + passowrdText);
        /*----------DB저장----------*/

        auth.SignInWithEmailAndPasswordAsync(nicknameText, passowrdText).ContinueWith(task =>
        {
            if (task.IsCanceled) //취소 시
            {  
                Title.text = "취소";
                return;
            }
            if (task.IsFaulted)  //문제가 있을 시
            {
                Title.text = "아이디 없음";
                return;
            }

            //로그인 완료 시 실행 
            Firebase.Auth.FirebaseUser newUser = task.Result;
            SceanChange = true;

            string user_email = NicknameInput.text; //이메일 받아와서 저장
            char sp = '@';
            string[] user_id = user_email.Split(sp);  //@ 뒷부분 삭제하여 id로 저장
            Player_Scr.player_name = user_id[0];  //
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

        });


    }

    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 접속 상태 표시
        connectionInfoText.text = "빈 방이 없음, 새로운 방 생성...";
        // 최대 4명을 수용 가능한 빈방을 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        // 접속 상태 표시
        connectionInfoText.text = "방 참가 성공";
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        // 모든 룸 참가자들이 Main 씬을 로드하게 함
        //PhotonNetwork.LoadLevel("MainGame_");
        //PhotonNetwork.LoadLevel("Main_map");
        PhotonNetwork.LoadLevel("NetGame");
    }

    //로그인 패널 비활성화, 로비 패널 활성화
    public void OnLogin()
    {
        LobbyPenel.SetActive(true);
        LoginPenel.SetActive(false);

    }
    

}