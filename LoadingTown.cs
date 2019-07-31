using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingTown : MonoBehaviour
{
    private AsyncOperation async; // 로딩
    private bool canOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Load");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCanOpen()
    {
        canOpen = true;
    }

    IEnumerator Load()
    {
        async = SceneManager.LoadSceneAsync("Town"); // 열고 싶은 씬
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
           //progress = async.progress;

            yield return true;

            if (canOpen)
                async.allowSceneActivation = true;
        }
    }
}