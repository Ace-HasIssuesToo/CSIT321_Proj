using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 roamPostion;
    Vector3 lastMoveDir;

    public GameObject player;
    public FieldOfView fov;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        Vector3 targetPosition = player.transform.position;
        Vector3 dirToTarget = (targetPosition - transform.position).normalized; 
        lastMoveDir = dirToTarget;

        fov.setOrigin(transform.position);
        fov.setLookDirection(lastMoveDir);
    }

    private Vector3 getRoamingPosition()
    {
        return startingPosition + getRandomDirection() * Random.Range(0.1f, 0.7f);
    }

    private static Vector3 getRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1, 1f)).normalized;
    }
}
