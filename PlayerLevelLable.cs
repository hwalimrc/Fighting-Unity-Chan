using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerLevelLable : MonoBehaviour
{
    Text levelLable;

    // Start is called before the first frame update
    void Start()
    {
        levelLable = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        levelLable.text = Player.level.ToString();       
    }
}
