using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyBase : MonoBehaviour
{
    public AIPath AiPath;

    public FieldOfView fov;
    public FieldOfView fov2;
    public Animator animator;

    /// <summary>Target points to move to in order</summary>
    public Transform[] targets;

    /// <summary>Time in seconds to wait at each target</summary>
    public float delay = 0;

    /// <summary>Current target index</summary>
    int index;

    IAstarAI agent;
    float switchTime = float.PositiveInfinity;

    private enum state
    {
        Normal,
        Caution,
        Alert,
    }

    private state currentState;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<IAstarAI>();
        currentState = state.Normal;
        settingFovSettings();
    }

    private void settingFovSettings()
    {
        fov.GetComponent<FieldOfView>().setFovAngle(45f);
        fov.GetComponent<FieldOfView>().setViewDistance(0.75f);

        fov2.GetComponent<FieldOfView>().setFovAngle(45f);
        fov2.GetComponent<FieldOfView>().setViewDistance(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (targets.Length == 0) return;

        bool search = false;


        Vector3 targetPosition = agent.destination;
        Vector3 dirToTarget = (targetPosition - transform.position).normalized;

        //fov.setOrigin(transform.position);
        fov.setLookDirection(dirToTarget);

        //fov2.setOrigin(transform.position);
        fov2.setLookDirection(dirToTarget);

        switch (currentState)
        {
            default:
            case state.Normal: 
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

                if (AiPath.reachedDestination)
                {
                    animator.SetFloat("Speed", 0);
                }
                else
                {
                    animator.SetFloat("Speed", 1);
                    animator.SetFloat("Horizontal", dirToTarget.x);
                    animator.SetFloat("Vertical", dirToTarget.y);
                }
                break;

            case state.Caution:
                break;

            case state.Alert:
                break;
        }
       
    }

}