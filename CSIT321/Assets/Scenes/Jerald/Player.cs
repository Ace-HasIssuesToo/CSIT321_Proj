using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    private Vector3 lastMoveDir;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector3 waypointDir = (transform.position).normalized;
        lastMoveDir = waypointDir;

        //if a movement key is being pressed ...
        if (movement != Vector2.zero)
        {
            //update horizontal/vertical accordingly
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }   //else Horizontal and Vertical will not update each frame, leaving it at last recorded value 

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //movement.normalized so character does not move faster diagonally
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
