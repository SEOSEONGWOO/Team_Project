using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    [SerializeField]
    private Transform characterBody;

    //땅체크
    private Transform groundCheck;
    public static bool grounded = true;

    Rigidbody rigdbody;
    bool isJumping = false;
    public float jumpPower = 3.5f;

    public static bool isAttack = false;
    public static bool isSkill1 = false;
    public static bool isSkill2 = false;



    public Animator avater;
    void Start()
    {
        avater = characterBody.GetComponent<Animator>();
        rigdbody = GetComponent<Rigidbody>();
        groundCheck = transform.Find("Ground");
        myRigid = GetComponent<Rigidbody>();  // private

        
    }

    void Update()  // 컴퓨터마다 다르지만 대략 1초에 60번 실행
    {
        
        grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Blocking"));
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("스페이스바");
            if (grounded == true)
            {
                Debug.Log("aaaaa");
                isJumping = true;
                avater.SetBool("idleToJump", Input.GetKey(KeyCode.Space));
            }

        }
        
         
        
        //공격 애니메이션 제어
        
        if (isAttack == true )
        {
            avater.SetBool("idleToAttack01", true);

            isAttack = false;
        }
        else
        {
            avater.SetBool("idleToAttack01", false);
            
        }



        //스킬 1 제어
        
        if (isSkill1 == true)
        {
            avater.SetBool("idleToSkill01", true);
            Debug.Log("스킬1 모션 실행");
            isSkill1 = false;
        }
        else
        {
            avater.SetBool("idleToSkill01", false);
        }


        
        if (isSkill2 == true)
        {
            avater.SetBool("idleToSkill02", true);
            Debug.Log("스킬2 모션 실행");
            isSkill2 = false;
        }
        else
        {
            avater.SetBool("idleToSkill02", false);
        }



        avater.SetBool("idleToRun", Input.GetKey(KeyCode.W));      
        //avater.SetBool("idleToSkill02", Input.GetMouseButtonDown(2));
        Move();                 // 1️⃣ 키보드 입력에 따라 이동
        CameraRotation();       // 2️⃣ 마우스를 위아래(Y) 움직임에 따라 카메라 X 축 회전 
        CharacterRotation();    // 3️⃣ 마우스 좌우(X) 움직임에 따라 캐릭터 Y 축 회전
    }

   

    void Attack()
    {
        avater.SetBool("idleToSkill01", Input.GetMouseButtonDown(1));
    }

    void FixedUpdate() // 리지드바디 이용할 경우 update 대신 FixedUpdate 사용
    {
        if (isJumping == true)
        {
            Debug.Log("bbbb");
            rigdbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            avater.SetBool("idleToJump", true);
            isJumping = false;

        }
        if (isJumping == false)
        {
            avater.SetBool("idleToJump", false);
        }

    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, 5 , cameraRotationLimit);
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()  // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 쿼터니언 * 쿼터니언
        // Debug.Log(myRigid.rotation);  // 쿼터니언
        // Debug.Log(myRigid.rotation.eulerAngles); // 벡터
    }

}