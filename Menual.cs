using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menual : MonoBehaviour
{
    public GameObject charMovement;
    public GameObject skill;

    public bool panel_1;
    public bool panel_2;

    // Start is called before the first frame update
    void Start()
    {
        panel_1 = false; panel_2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (panel_1 == true)
            charMovement.SetActive(true);
        else charMovement.SetActive(false);

        if (panel_2 == true)
            skill.SetActive(true);
        else skill.SetActive(false);
    }

    public void Panel_1()
    {
        panel_1 = true;
    }

    public void Panel_1_close()
    {
        panel_1 = false;
    }

    public void Panel_2()
    {
        panel_2 = true;
    }

    public void Panel_2_close()
    {
        panel_2 = false;
    }
}