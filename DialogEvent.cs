using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogEvent : MonoBehaviour
{
    public GameObject Panel;
    public List<GameObject> DialogList = new List<GameObject>();
    private int dialogNumber = 0;
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Panel.SetActive(false);
        }
    }

    public void Dialog()
    {
        if (dialogNumber == 0)
        {
            DialogList[dialogNumber].SetActive(true);
            dialogNumber++;
        }

        else
        {
            DialogList[dialogNumber - 1].SetActive(false);
            DialogList[dialogNumber].SetActive(true);
            dialogNumber++;
        }
    }


}