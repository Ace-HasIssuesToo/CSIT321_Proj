using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPatrol : MonoBehaviour
{
    Vector3 startingPosition;
    public FieldOfView fov;

    public float countdown = 10;

    IAstarAI agent;
    float switchTime = float.PositiveInfinity;

    private void Start()
    {
        startingPosition = transform.position;
        agent = GetComponent<IAstarAI>();
    }

    private void Update()
    {
        bool search = false;
        bool setDestination = false;

        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            search = true;
            setDestination = true;
            countdown = 10;
        }

        if(setDestination) agent.destination = getRoamingPosition();


        if (search) agent.SearchPath();

        Vector3 targetPosition = agent.destination;
        Vector3 dirToTarget = (targetPosition - transform.position).normalized;

        fov.setOrigin(transform.position);
        fov.setLookDirection(dirToTarget);
    }

    private Vector3 getRoamingPosition()
    {
        return startingPosition + getRandomDirection() * Random.Range(0.5f, 2f);
    }

    private Vector3 getRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
}
