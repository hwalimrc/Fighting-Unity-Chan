using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightingMovement : MonoBehaviour
{
    private Rigidbody rbody;
    private Animator ani;
    private float speed = 5.0f;
    private int skillNum;

    float rotateSpeed = 2f;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    private bool jab;
    private bool risingPunch;
    private bool hiKick;
    private bool screwKick;

    private bool lightning;
    private bool starlight;
    private bool moonlight;

    private bool damaged;

    public static bool isAttack; // 공격을 가했을 때.
    private bool isLose = false;

    public static bool isAttacked; // 공격을 받았을 때.

    public GameObject player;

    public GameObject lightningEffect;
    public GameObject starlightEffect;
    public GameObject moonlightEffect;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>(); //컴포넌트를 가져온다.
        ani = GetComponent<Animator>();

        ani.Play("Land");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");//좌우 입력. -1이 왼쪽. 1이 오른쪽
        float v = Input.GetAxisRaw("Vertical"); //상하 입력. -1이 아래, 1이 위
        
        if (!(h == 0 && v == 0)) //방향키를 입력한경우
        {
            //ani.SetFloat("Direction", -1);
            Vector3 move = new Vector3(h, 0, v); //볼 방향을 가리킨다.
            Quaternion dir = Quaternion.LookRotation(move.normalized);//해당 방향을 보도록 회전하는 Quaternion 변수 생성
            dir.x = 0; //몸체 방향은 y축만 회전하면 되므로 x,z축은 0으로 강제고정.
            dir.z = 0;
            transform.rotation = dir;//현재 객체의 방향을 생성한 Quaternion 변수로 맞춤.
        }

        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyUp(KeyCode.Alpha1)) { isAttack = true; jab = true; }
        if (Input.GetKeyUp(KeyCode.Alpha2)) { isAttack = true; risingPunch = true; }
        if (Input.GetKeyUp(KeyCode.Alpha3)) { isAttack = true; hiKick = true; }
        if (Input.GetKeyUp(KeyCode.Alpha4)) { isAttack = true; screwKick = true; }
        if (Input.GetKeyUp(KeyCode.Alpha5)) { isAttack = true; lightning = true; }
        if (Input.GetKeyUp(KeyCode.Alpha6)) { isAttack = true; starlight = true; }
        if (Input.GetKeyUp(KeyCode.Alpha7)) { isAttack = true; moonlight = true; }

        if (Enemy.isAttack == true) { damaged = true; }

        if (Player.currentHP == 0) { isLose = true; }    
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(horizontalMove, 0, verticalMove);
        rbody.MovePosition(rbody.position + move * speed * Time.deltaTime); //이동할 위치 = 현재 위치 + 위치값

        if (horizontalMove == 0 && verticalMove == 0)
        { ani.SetBool("RunChk", false); }//방향키가 입력되있지 않은경우 현재 이동상태를 false로 둔다.

        else
        {
            ani.SetBool("RunChk", true); //방향키가 입력된경우 현재 이동상태를 true로 둔다.       
            //isAttack = false;
        }

        Jab();
        RisingPunch();
        Hikick();
        ScrwKick();
        Lightning();

        Lose();
        //Damaged();
    }

    void Run()
    {
        movement.Set(horizontalMove, 0, verticalMove);
        movement = movement.normalized * speed * Time.deltaTime;

        rbody.MovePosition(transform.position + movement);

        if (horizontalMove == 0 && verticalMove == 0) //방향키가 입력되있지 않은경우 현재 이동상태를 false로 둔다.
        { ani.SetBool("RunChk", false); }

        else //방향키가 입력된경우 현재 이동상태를 true로 둔다.
        { ani.SetBool("RunChk", true); }
    }

    public void Jab()
    {
        if (!jab && !isAttack) return;
        ani.Play("Jab", -1, 0);
        Enemy.isAttack = true;
        isAttack = false;
        jab = false;
    }

    public void RisingPunch()
    {
        if (!risingPunch && !isAttack) return;
        ani.Play("RISING_P", -1, 0);
        Enemy.isAttack = true;
        isAttack = false;
        risingPunch = false;
    }

    public void Hikick()
    {
        if (!hiKick && !isAttack) return;
        ani.Play("Hikick", -1, 0);
        Enemy.isAttack = true;
        isAttack = false;
        hiKick = false;
    }

    public void ScrwKick()
    {
        if (!screwKick && !isAttack) return;
        ani.Play("ScrewKick", -1, 0);
        Enemy.isAttack = true;        
        isAttack = false;
        screwKick = false;
    }

    public void Lightning()
    {
        if (!lightning && !isAttack) return;
        ani.Play("Spinkick", -1, 0);
        RobotController.enemyObject = Instantiate(lightningEffect, transform.position, transform.rotation) as GameObject;
        Enemy.isAttack = true;
        isAttack = false;
        lightning = false;
    }

    public void Attacked()
    {
        if (!isAttacked) return;
        //ani.Play("DAMAGED00", -1, 0);
        Player.myHP = Player.myHP - 50;
        isAttacked = false;
    }

    public void Lose()
    {
        if (!isLose) return;
        if (isLose == true) SceneManager.LoadScene("Lose");
        isLose = false;
    }

    public void Stand()
    {
        ani.Play("Idle", -1, 0);
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2.0f);
    }

    public void destoryEffect()
    {

    }
}