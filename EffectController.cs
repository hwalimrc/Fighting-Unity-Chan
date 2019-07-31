using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectController : MonoBehaviour
{
    private Transform EffectTransform;
    private Transform EnemyTransform;
    private NavMeshAgent nvAgent;

    // Start is called before the first frame update
    void Start()
    {
        EffectTransform = this.gameObject.GetComponent<Transform>();
        EnemyTransform = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        nvAgent.destination = EnemyTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
