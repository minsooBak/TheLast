using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slow : MonoBehaviour
{
    private NavMeshAgent Agent;
    private float speed;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        SlowDown();
        Invoke("StopSlow",3.5f);
    }
    private void SlowDown()
    {
        speed = Agent.speed;
        Agent.speed = speed / 2;
    }
    private void StopSlow()
    {
        Agent.speed = speed;
        Destroy(GetComponent<Slow>());
    }
}
