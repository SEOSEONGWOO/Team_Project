using UnityEngine;
using System.Collections;

public class CHARACTER_Controller : MonoBehaviour
{
    
    private Transform groundCheck;
    public bool grounded = false;

    public float jumpPower = 0.8f;
    Rigidbody rigdbody;
    bool isJumping;


    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    public Animator avater;
    void Start()
    {
        
        avater = characterBody.GetComponent<Animator>();
        rigdbody = GetComponent<Rigidbody>();
        groundCheck = transform.Find("GoundCheck");
    }

    private void OnEventFx(GameObject InEffect)
    {
        GameObject newSpell = Instantiate(InEffect);

        Destroy(newSpell, 1.0f);
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Space) && grounded == true)
        {
            isJumping = true;
            avater.SetBool("idleToJump", Input.GetKey(KeyCode.Space));

        }

        LookAround();
        Move();

        avater.SetBool("idleToRun", Input.GetKey(KeyCode.W));
        avater.SetBool("idleToAttack01", Input.GetMouseButtonDown(0));
        avater.SetBool("idleToSkill01", Input.GetMouseButtonDown(1));
        avater.SetBool("idleToSkill02", Input.GetMouseButtonDown(2));
        

    }
    void FixedUpdate() // 리지드바디 이용할 경우 update 대신 FixedUpdate 사용
    {
        if (isJumping == true)
        {
            rigdbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            avater.SetBool("idleToJump", true);
            isJumping = false;

        }
        if (isJumping == false)
        {
            avater.SetBool("idleToJump", false);
        }

    }

    //캐릭터 이동 함수
    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        avater.SetBool("idleToRun", isMove);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 3f;
        }
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

    }


    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 30f);
        }
        else
        {
            x = Mathf.Clamp(x, 355f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);

    }
}
