using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnit : MonoBehaviour
{
    public float fAniTime = 10f;
    private float fTimeCalc = 10f;

    public GameObject create;
    public GameObject obj;
    public int iValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        create = Instantiate(obj, transform.position, transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void getUnit()
    { create = (GameObject)Instantiate(obj, transform.position, transform.rotation) as GameObject; }
}