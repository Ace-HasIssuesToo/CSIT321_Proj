using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public FieldOfView fov;

    /// <summary>Target points to move to in order</summary>
    public Transform[] targets;

    /// <summary>Time in seconds to wait at each target</summary>
    public float delay = 0;

    /// <summary>Current target index</summary>
    int index;

    IAstarAI agent;
    float switchTime = float.PositiveInfinity;

    private void Start()
    {
        agent = GetComponent<IAstarAI>();
    }

    private void Update()
    {
        if (targets.Length == 0) return;

        bool search = true;

        // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
        // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
        if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime))
        {
            switchTime = Time.time + delay;
        }

        if (Time.time >= switchTime)
        {
            index = index + 1;
            search = true;
            switchTime = float.PositiveInfinity;
        }

        index = index % targets.Length;
        agent.destination = targets[index].position;
        

        if (search) agent.SearchPath();

        Vector3 targetPosition = agent.destination;
        Vector3 dirToTarget = (targetPosition - transform.position).normalized; 

        fov.setOrigin(transform.position);
        fov.setLookDirection(dirToTarget);
    }
}
