using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMoveAgent : MonoBehaviour
{

    private readonly float traceSpeed = 2.0f;
    private NavMeshAgent agent;

    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            TraceTarget(_traceTarget);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; // 목적지에 가까워질수록 속도를 줄이는 옵션 비활성화
        agent.speed = traceSpeed;

    }
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;

        agent.destination = pos;
        agent.isStopped = false;
    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
