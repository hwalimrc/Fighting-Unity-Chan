using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileMovement : MonoBehaviour
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
    private bool isLose = false;
    private bool isRun = false;

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
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (Enemy.isAttack == true) { damaged = true; }

        if (Player.currentHP == 0) { isLose = true; }
    }

    void FixedUpdate()
    {
        if (MobileController.charMove == false)
        { ani.SetBool("RunChk", false); } //방향키가 입력되있지 않은경우 현재 이동상태를 false로 둔다.

        else
        {
            ani.SetBool("RunChk", true); //방향키가 입력된경우 현재 이동상태를 true로 둔다. 
        }

        Jab();
        RisingPunch();
        Hikick();
        ScrwKick();
        Lightning();
        
        Lose();
        //Damaged();
    }

    public void getJab()
    { Player.isAttack = true; jab = true; }

    public void getPunch()
    { Player.isAttack = true; risingPunch = true; }

    public void getKick()
    { Player.isAttack = true; hiKick = true; }

    public void getS_Kick()
    { Player.isAttack = true; screwKick = true; }

    public void Jab()
    {
        if (!jab && !Player.isAttack) return;
        ani.Play("Jab", -1, 0);
        Enemy.isAttack = true;
        Player.isAttack = false;
        jab = false;
    }

    public void RisingPunch()
    {
        if (!risingPunch && !Player.isAttack) return;
        ani.Play("RISING_P", -1, 0);
        Enemy.isAttack = true;
        Player.isAttack = false;
        risingPunch = false;
    }

    public void Hikick()
    {
        if (!hiKick && !Player.isAttack) return;
        ani.Play("Hikick", -1, 0);
        Enemy.isAttack = true;
        Player.isAttack = false;
        hiKick = false;
    }

    public void ScrwKick()
    {
        if (!screwKick && !Player.isAttack) return;
        ani.Play("ScrewKick", -1, 0);
        Enemy.isAttack = true;
        Player.isAttack = false;
        screwKick = false;
    }

    public void Lightning()
    {
        if (!lightning && !Player.isAttack) return;
        ani.Play("Spinkick", -1, 0);
        RobotController.enemyObject = Instantiate(lightningEffect, transform.position, transform.rotation) as GameObject;
        Enemy.isAttack = true;
        Player.isAttack = false;
        lightning = false;
    }

    public void Attacked()
    {
        if (!Player.isAttacked) return;
        //ani.Play("DAMAGED00", -1, 0);
        Player.myHP = Player.myHP - 50;
        Player.isAttacked = false;
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
