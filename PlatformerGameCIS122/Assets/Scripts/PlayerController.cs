// Created by: Jeremy Fischer 11/3/2021
// Edited by (Add name if you do anything to this script): Jeremy Fischer, Madeline Ellingson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private Rigidbody2D rb;

    private Collider2D coll;
    [SerializeField] private LayerMask Ground;

    private Animator anim;

    private enum State { idle, run, jump, falling};
    private State state = State.idle;

    // Serialized and Public Variables
    [SerializeField] float moveForce = 5.0f;
    [SerializeField] float jumpForce = 10.0f; 
     

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the horizontal input, from the general unity input
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection > 0)
        {
            rb.velocity = new Vector2(moveForce, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (hDirection < 0)
        {
            rb.velocity = new Vector2(-moveForce, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
          //  rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jump;
        }

        VelocityState();
        anim.SetInteger("State", (int)state);
    }

    // Determines the animation played based off the state of the character
    private void VelocityState()
	{
        if (state == State.jump)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(Ground))
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.run;
        }
        else
        {
            state = State.idle;
        }
    }
}
