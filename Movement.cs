using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody rbody;
    private Animator ani;
    private GameObject playerModel;

    private float speed = 5.0f;
    private int skillNum;

    float rotateSpeed = 2f;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool isJumping;
    private bool isLose = false;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>(); //컴포넌트를 가져온다.
        ani = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨기기
        Cursor.visible = false;
    }

    void Update()
    {       
        float h = Input.GetAxisRaw("Horizontal");//좌우 입력. -1이 왼쪽. 1이 오른쪽
        float v = Input.GetAxisRaw("Vertical"); //상하 입력. -1이 아래, 1이 위

        if (!(h == 0 && v == 0)) //방향키를 입력한경우
        {
            ani.SetFloat("Direction", -1);
            Vector3 move = new Vector3(h, 0, v); //볼 방향을 가리킨다.
            Quaternion dir = Quaternion.LookRotation(move.normalized);//해당 방향을 보도록 회전하는 Quaternion 변수 생성
            dir.x = 0; //몸체 방향은 y축만 회전하면 되므로 x,z축은 0으로 강제고정.
            dir.z = 0;
            transform.rotation = dir;//현재 객체의 방향을 생성한 Quaternion 변수로 맞춤.
        }
        
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        { isJumping = true; }

        if (Player.myHP <= 0) isLose = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(horizontalMove, 0, verticalMove);
        rbody.MovePosition(rbody.position + move * speed * Time.deltaTime); //이동할 위치 = 현재 위치 + 위치값

        if (horizontalMove == 0 && verticalMove == 0)
        { ani.SetBool("RunChk", false); }//방향키가 입력되있지 않은경우 현재 이동상태를 false로 둔다.

        else
        {
            ani.SetBool("RunChk", true); //방향키가 입력된경우 현재 이동상태를 true로 둔다.        
        }

        Jump();
    }

    void Run()
    {
        movement.Set(horizontalMove, 0, verticalMove);
        movement = movement.normalized * speed * Time.deltaTime;

        rbody.MovePosition(transform.position + movement);

        if (horizontalMove == 0 && verticalMove == 0) //방향키가 입력되있지 않은경우 현재 이동상태를 false로 둔다.
        { ani.SetBool("RunChk", false); }

        else //방향키가 입력된경우 현재 이동상태를 true로 둔다.
        {
            ani.SetBool("RunChk", true);
        }
    }

    void Jump()
    {
        if (!isJumping) return;

        ani.Play("JUMP00", -1, 0);

        isJumping = false;
    }
}