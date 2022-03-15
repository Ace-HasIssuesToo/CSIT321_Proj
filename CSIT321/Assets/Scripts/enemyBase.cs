using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBase : MonoBehaviour
{
    float moveSpeed = 5f;
    public Rigidbody2D rb;
   
    private enum state
    {
        Normal,
        Caution,
        Alert,
    }

    private state currentState;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            default:
            case state.Normal:
                break;

            case state.Caution:
                break;

            case state.Alert:
                break;
        }
    }
}
