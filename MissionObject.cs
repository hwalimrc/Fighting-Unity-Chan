using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObject : MonoBehaviour
{
    public GameObject Object;
    private bool isActive;

    void Start()
    {
        Object.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (isActive == false) // 현재 게임이 진행중일 때,
                isActive = true;
            else if (isActive == true) // 현재 게임이 멈췄을 때,
                isActive = false;
            else
                Debug.Log("error");
        }

        ActiveDialog(isActive);
    }

    void ActiveDialog(bool active)
    {
        if (active == true) // 게임이 멈췄을 때,
        {
            Object.SetActive(true); // 대화창 활성화
        }

        else // 게임이 진행중일 때,
        {
            Object.SetActive(false); // 대화창 비활성화
        }
    }

    public bool Paused()
    {
        if (isActive == true)
            return true;
        else
            return false;
    }
}