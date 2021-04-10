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
    public float initialJumpForce;
    public float additionalJumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    public float doubleTapThreshold = 0.5f;
    private float lastTapTimeRight = 0f;
    private float lastTapTimeLeft = 0f;

    public float dashForce;




    void Start()
    {
        controller.m_Grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        inputAxis = Input.GetAxisRaw("Horizontal");
        horizontalMove += inputAxis * accelRate;

        if(rb.velocity.x > 0f) //decel bools using the rb.velocity so we know which direction we need to decelerate
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
        //automatically decellerate if neither a or d is pressed.
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

        //jump logic
        if (controller.m_Grounded == true && Input.GetButtonDown("Jump"))
        {
            jump = true;
            jumpTimeCounter = jumpTime;
            rb.AddForce(new Vector2(0f, initialJumpForce));
        }
        
        
        if (jump && Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0)
            {
                rb.AddForce(new Vector2(0f, additionalJumpForce));
                jumpTimeCounter -= Time.fixedDeltaTime;
            }
            else if (jumpTimeCounter < 0)
            {
                jump = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
        }

        //dash right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if ((Time.time - lastTapTimeRight) < doubleTapThreshold)
            {
                rb.AddForce(new Vector2(dashForce, 0f));
            }
            lastTapTimeRight = Time.time;

        }
        //dash left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if ((Time.time - lastTapTimeLeft) < doubleTapThreshold)
            {
                rb.AddForce(new Vector2(dashForce * -1.0f, 0f));
            }
            lastTapTimeLeft = Time.time;
        }

    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

}
