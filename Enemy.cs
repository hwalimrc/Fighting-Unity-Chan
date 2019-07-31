using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{   
    public Vector3 direction;
    public int iValue = 5; // 공격 행동패턴 발동 기준 확률

    public static bool isAttack; // 공격을 받았을 때의 상태.

    private Animator enemyAni;
    private Rigidbody enemyRbody;
    public GameObject enemyObject;

    private Transform playerChar; // 플레이어 캐릭터의 위치
    private Transform enemyTransform;

    private NavMeshAgent nvAgent;
    private float distance; 

    public float enemyHp;

    // Start is called before the first frame update
    void Start()
    {
        enemyAni = GetComponent<Animator>();
        enemyRbody = GetComponent<Rigidbody>();
        playerChar = GetComponent<Transform>();
        enemyTransform = GetComponent<Transform>();
        Reverse();

        enemyHp = 1000;
    }

    // Update is called once per frame
    void Update()
    {      
        distance = Vector3.Distance(playerChar.transform.position, enemyTransform.transform.position);

        if (distance <= 50)
        {
            MoveForTarget();
        }
        else { Menace(); Stand(); }
    }

    void FixedUpdate()
    {
        //Attack();    
        if (enemyHp <= 0) Death(); // 적 NPC의 HP가 모두 소진됬을 경우.
    }

    private void OnTriggerEnter(Collider other) // 플레이어와 적 캐릭터가 충돌했을 경우
    {
        //enemyAni.SetBool("Walking", true);
        if (other.gameObject.name == "Player")
        {
            Stand();
            if (isAttack == true) // 공격을 받았을 때(플레이어가 공격 행동을 실행했을 경우)
            {
                enemyHp = enemyHp - 100;
                if (Random.Range(1, 10) <= iValue) // 일정 확률에 만족하면
                {
                    enemyAni.Play("Attack", -1, 0); // 반격 행동 패턴을 실행한다.    
                    FightingMovement.isAttacked = true;
                    isAttack = false; // 피격 상태 초기화   
                    Player.currentHP = Player.currentHP - 50;
                    Walk();
                }
                else
                {
                    enemyAni.Play("Damage", -1, 0); // 피격 행동 패턴을 실행한다.                   
                    isAttack = false; // 피격 상태 초기화.
                    Walk();
                }
            }

            else // 플레이어가 공격을 하지 않았을 경우
            {
                if (Random.Range(1, 10) <= iValue) // 일정 확률에 만족하면
                {
                    Menace(); // 위협 행동 패턴을 실행한다.
                    Walk();
                }

                else
                {
                    enemyAni.Play("Attack", -1, 0); // 공격 행동 패턴을 실행한다.    
                    FightingMovement.isAttacked = true;
                    Walk();
                }
            }
        }
    }

    public void MoveForTarget()
    {
        enemyTransform = this.gameObject.GetComponent<Transform>();
        playerChar = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        nvAgent.destination = playerChar.position; // 추적 대상의 위치를 설정하여 추적을 시작한다.
        Walk();
        if (distance <= 10) { Stand(); nvAgent.speed = 0; }
        else nvAgent.speed = 5;
    }

    public void Death()
    { enemyAni.Play("Death"); Destroy(enemyObject, 2f); }

    public void Reverse()
    { enemyAni.Play("Down_StandUp", -1, 0); }

    public void Stand()
    { enemyAni.SetBool("Walking", false); }

    public void Walk()
    { enemyAni.SetBool("Walking", true); }

    public void Menace()
    { enemyAni.Play("Matinee_Walk_Pose"); }
}