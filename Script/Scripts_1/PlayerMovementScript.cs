using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovementScript : MonoBehaviour {

    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    public PhotonView PV;
    public Animator AN;
    public Rigidbody RB;
    

    void Awake()
    {
        
    }

    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(RB); //이거 해주고 fixed Update에다가 추가해줘야 됨 모르겠어.. 두개의 물리엔진인가봄

        }
       
    }

    void Update()
    {
        if (PV.IsMine)
        {
            Look();
            Move();
            Jump();
        }
        
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity); //y축 회전
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;//다시다시다시다시
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);//최대 최소 설정

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;

        
    }

    void Move()
    {
       
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        if (moveDir != Vector3.zero)
        {
            AN.SetBool("walk", true);
        }
        else
        {
            AN.SetBool("walk", false);
        }
    }
    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) //점프구현
        {
            Debug.Log("점프");
            RB.velocity = Vector3.zero;
            RB.AddForce(Vector3.up * 400);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        Debug.Log("바닥상태 변경!");
        grounded = _grounded;
        Debug.Log(grounded);
    }

    void FixedUpdate() //찾아보자
    {
        if (!PV.IsMine)
        {
            return;
        }
        RB.MovePosition(RB.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
