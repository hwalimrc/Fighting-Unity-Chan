using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour
{
    // Public
    public Transform Player; // 플레이어
    public Transform Stick; // 컨트롤러
    public static bool charMove;

    // Private
    private Vector3 StickFirstPos; // 컨트롤러의 초기 위치.
    private Vector3 JoyVec; // 컨트롤러의 방향.
    private float Radius; // 컨트롤러 배경의 반지름.
    private bool MoveFlag;

    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;

        // 캔버스 크기에대한 반지름 조절.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
        MoveFlag = false;
    }

    void Update()
    {
        if (MoveFlag)
            Player.transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        getMove();
    }

    public void Drag(BaseEventData _Data)
    {
        MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        JoyVec = (Pos - StickFirstPos).normalized;
        // 컨트롤러를 이동시킬 방향을 구한다.(상,하,좌,우)

        float Dis = Vector3.Distance(Pos, StickFirstPos);

        if (Dis < Radius)  Stick.position = StickFirstPos + JoyVec * Dis;
        // 거리가 반지름보다 작을 경우, 컨트롤러를 현재 터치하고 있는 지점으로 이동.
        // 거리가 반지름보다 ㅋ큰 경우, 컨트롤러를 반지름의 크기만큼만 이동.

        else Stick.position = StickFirstPos + JoyVec * Radius;

        Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
    }

    // 드래그 끝.
    public void DragEnd()
    {
        Stick.position = StickFirstPos; // 스틱을 원래의 위치로.
        JoyVec = Vector3.zero;          // 방향을 0으로.
        MoveFlag = false;
    }

    public void getMove()
    {
        if (MoveFlag) charMove = true;
        else charMove = false;
    }
}