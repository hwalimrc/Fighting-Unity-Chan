using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static float myHP = 100;
    public static float currentHP = 100;

    public static float myTP = 500;

    public Slider hpSlider;
    public static int level = 1;

    public static bool isAttack;// 공격 판정 여부,
    public static bool isAttacked; // 피격 판정 여부.

    public int str; // 공격력: 플레이어 캐릭터의 기본 공격력 수치에 정비례
    public int def; // 방어력: 플레이어 캐릭터의 체력 수치에 정비례
    public int dex; // 민첩성: 플레이어 캐릭터의 회피 수치에 정비례
    public int mag; // 마력: 플레이어 캐릭터의 쿨타임 수치에 반비례

    // Start is called before the first frame update
    void Start()
    {
        isAttack = false; isAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.maxValue = myHP;
        hpSlider.value = currentHP;
    }
}