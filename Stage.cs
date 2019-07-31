using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    void OnTriggerEnter(Collider _col)  // 트리거에 충돌이 되었을 때는 이 함수를 도출한다.
    {
        if (_col.gameObject.name == "Player")
        {
            SceneChange.stageNumber = 1;
            SceneManager.LoadScene("StagePreview");
        }
    }
}