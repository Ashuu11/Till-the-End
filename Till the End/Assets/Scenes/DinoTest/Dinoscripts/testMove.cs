using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] points;
    Vector3 pos;
    NavMeshAgent agent;
    
    void Start()
    {
        pos = points[Random.Range(0, points.Length)].position;
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(pos);
        if(agent.remainingDistance<agent.stoppingDistance)
            pos = points[Random.Range(0, points.Length)].position;
    }
}
