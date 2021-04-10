using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D rb; 
    float horizontalMove = 0f;
    bool jump = false;

    public float maxSpeed;
    public float decelRate;
    public float accelRate;
    float forwardVelocity;
    float inputAxis;
    bool movingFoward, movingBack;


    // Update is called once per frame
    void Update()
    {
        inputAxis = Input.GetAxisRaw("Horizontal");
        horizontalMove += inputAxis * accelRate;

        if(rb.velocity.x > 0f) //decel bools so we know which direction we need to decelerate
        {

            movingFoward = true;
            movingBack = false;
        }
        if(rb.velocity.x < 0f)
        {

            movingFoward = false;
            movingBack = true;
        }

        if(inputAxis == -1.0f) //max speed check
        {

            if(horizontalMove < maxSpeed * -1.0f)
            {
                horizontalMove = maxSpeed * -1.0f;
            }
        }
        if (inputAxis == 1.0f)
        {
            if(horizontalMove > maxSpeed)
            {
                horizontalMove = maxSpeed;
            }
        }
        if (inputAxis == 0f)
        {

            if (movingFoward)
            {
                horizontalMove -= decelRate;
                horizontalMove = Mathf.Max(horizontalMove, 0);
               
            }
            else if (movingBack)
            {
                horizontalMove += decelRate;
                horizontalMove = Mathf.Min(horizontalMove, 0);
               
            }
            
            
        }


            if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        jump = false;
    }
}
