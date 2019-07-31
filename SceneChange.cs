using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public static int stageNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void firstStageLoad()
    {
        SceneManager.LoadScene("TownPreview");
    }

    public void startGame()
    {
        SceneManager.LoadScene("Openning");
    }

    public void Creator()
    {
        SceneManager.LoadScene("Creator");
    }

    public void Menual()
    {
        SceneManager.LoadScene("UserGuide");
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void Exit() // 빌드하면 오류생김.
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void Retry()
    {
        switch(stageNumber)
        {
            case 0:
                SceneManager.LoadScene("TownPreview");
                break;
            case 1: // 1스테이지에서 게임 오버 시.
                SceneManager.LoadScene("StagePreview");
                break;

            case 2: // 2스테이지에서 게임 오버 시.
                SceneManager.LoadScene("StagePreview");
                break;

            case 3: // 3스테이지에서 게임 오버 시.
                SceneManager.LoadScene("StagePreview");
                break;

            default: // Default.
                SceneManager.LoadScene("TitleScreen");
                break;
        }
    }
}
