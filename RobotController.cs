using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class RobotController : MonoBehaviour
{
    private Animator robotAni;
    private Rigidbody robotRbody;

    public static GameObject enemyObject;

    private Transform playerChar; // 플레이어 캐릭터의 위치.
    private Transform robotTransform; // 로봇 캐릭터의 위치.
    private Transform FirstTransform;

    private bool robotAttack; // 로봇 공격판정

    private NavMeshAgent nvAgent;
    private float robotHP; // 로봇의 체력은 개체마다 달라야 하므로 지역변수로 선언, 초기화x
    public float setting; // 로봇 체력값을 세팅해주는 변수.
    public int iValue = 5; // 공격 행동패턴 발동 기준 확률

    public bool robotAttackTurn = false; // 로봇이 공격을 할때 true가 됨.
    private float distance;
    // Start is called before the first frame update

    public bool tagToPlayer;

    void Start()
    {
        robotAni = GetComponent<Animator>(); // 애니메이터 지정
        robotRbody = GetComponent<Rigidbody>(); // 대상에 물리효과를 부여
        playerChar = GetComponent<Transform>(); // 플레이어 캐릭터 위치를 받아옴
        robotTransform = GetComponent<Transform>(); // AI캐릭터의 위치를 받아옴
        FirstTransform = robotTransform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(playerChar.transform.position, robotTransform.transform.position);
        Idle();
    }

    void Idle()
    {
        Stand();
        if (distance <= 20) // AI캐릭터의 시야 범위 내에 플레이어가 있을 경우
        { Chase(); } // 추적 행동을 전개

        else
        {
            if (tagToPlayer == true) // 플레이어와 접촉했을 경우.
            {
                if (FightingMovement.isAttack == true) // 플레이어가 공격을 했을 경우
                { Attack(); } // AI캐릭터도 공격 행동을 전개

                else
                {
                    if (Random.Range(1, 10) <= iValue)
                    { Meance(); }
                    else Attack();
                }
            }
            // 플레이어와 접촉하지 않았을 경우
            else; //Return(); // 원래 있던 위치로 돌아감.
        }
    }

    public void Chase()
    {
        robotTransform = this.gameObject.GetComponent<Transform>();
        playerChar = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        nvAgent.destination = playerChar.position; // 추적 대상의 위치를 설정하여 추적을 시작한다.
        Walk();
        if (distance <= 10) { nvAgent.speed = 0; }
        else nvAgent.speed = 5;
    }

    public void Stand() // 공격을 중지하고 서 있는 상태
    {
        robotAni.SetBool("Walking", false);
    }

    public void Attack() // 플레이어와 AI캐릭터가 교전을 벌이는 상태
    {
        if (robotHP > 0) // 적 캐릭터의 HP가 0보다 큰 경우
        {
            if (Player.currentHP > 0) // 플레이어가 살아있는 경우
            {
                robotAni.Play("anim_open_GoToRoll", -1, 0);
            }
            else Return();
        }
        else Death();
            
    }

    public void Meance() // AI캐릭터가 플레이어를 위협하는 행동을 전개
    {

    }


    public void Walk() // AI캐릭터가 걷기 모션을 취함.
    {
        robotAni.SetBool("Walking", true);
    }

    public void Return()
    {      
        nvAgent.destination = FirstTransform.position; // 첫 위치로 돌아간다.
        Walk();
    }

    public void Death()
    { robotAni.Play("anim_close", -1, 0); Destroy(enemyObject, 2f); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        { tagToPlayer = true; }
        else tagToPlayer = false;
    }
}
