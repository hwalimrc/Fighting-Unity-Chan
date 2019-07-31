using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MobileEnemy : MonoBehaviour
{
    // Private
    public GameObject enemyObject; // 적 유닛 모델링
    private Animator enemyAni; // 적 유닛 애니메이션
    private Rigidbody rigidBody; // 적 유닛 물리앤진
    private Transform enemyTransform; // 적 유닛 위치 좌표
    private Transform playerTransform; // 플레이어 캐릭터 위치 좌표
    private NavMeshAgent nvAgent; // 네비게이션 컨트롤러
    private float distance; // 적 유닛과 플레이어 사이의 거리
    private float speed = 5.0f;
    private float sight = 50.0f;
    private bool tagToPlayer;

    // Public
    public static bool isAttack; // 공격을 받았을 때의 상태.
    public float enemyHp; // 적 유닛의 HP

    // Start is called before the first frame update
    void Start()
    {
        enemyAni = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        enemyTransform = GetComponent<Transform>(); // 애니메이션, 물리앤진, 위치를 연동.
        tagToPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        Idle();
        // 플레이어 캐릭터와 적 NPC 캐릭터의 간격이
        // 적 NPC에 부여된 사야 안에 있는가?
        if (distance < sight) //yes
        {
            // 적 NPC가 플레이어 캐릭터를 추적
            Chase();
        }
        else // no
        {
            // 적 NPC 캐릭터와 플레이어 캐릭터가
            // 충돌하였는가?
            if (tagToPlayer == true) // yes
            {
                // 적 NPC 캐릭터가 플레이어와 접촉하였을 경우
                // 전투 상태로 돌입한다.
                SceneManager.LoadScene("Lose");
            }
            else // no
            { Return(); }
        }
    }

    public void Idle()
    { enemyAni.SetBool("Walking", false); }

    public void Chase()
    {
        enemyTransform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        nvAgent.destination = playerTransform.position; // 추적 대상의 위치를 설정하여 추적을 시작한다.
        Walk();
        nvAgent.speed = 5;
    }

    public void Walk()
    { enemyAni.SetBool("Walking", true); }

    public void Return()
    {
        //transform.position = Vector3.Lerp (transform.position, targetPosition, speed * Time.deltaTime);
    }

    // NPC 캐릭터가 다른 오브젝트와 충돌했을 경우.
    private void OnTriggerEnter(Collider other) 
    {
        // 플레이어 캐릭터와 충돌 했을 경우
        if (other.gameObject.name == "Player") 
        { SceneManager.LoadScene("Lose"); }
        else Return();
    }

}
