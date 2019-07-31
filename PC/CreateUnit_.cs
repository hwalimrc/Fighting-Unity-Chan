using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnit_ : MonoBehaviour
{
    public GameObject createUnit1;

    void Start()
    { 
        createUnit1.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            createUnit1.SetActive(true);
        }
    }
}