using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D rb;
    public ParticleSystem jumpPS;
    public ParticleSystem dustRightPS;
    public ParticleSystem dustLeftPS;


    float horizontalMove = 0f;
    bool jump = false;

    public float maxSpeed;
    public float maxAirSpeed;
    public float decelRate;
    public float accelRate;
    float forwardVelocity;
    float inputAxis;
    bool movingFoward, movingBack;
    public float initialJumpForce;
    public float additionalJumpForce;
    public float doubleJumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    public float doubleTapThreshold = 0.5f;
    public float doubleJumpThreshold = 1.5f; 
    private float lastTapTimeRight = 0f;
    private float lastTapTimeLeft = 0f;
    private float lastTapTimeJump = 0f;

    public float dashForce;
    private bool doubleJump;
    private bool dashCooldown = true;
    public float dashCooldownTime;
    public float doubleJumpCooldownTime;
    private bool jumpCooldown = true;

    private float staticMaxSpeed;
    private float staticMaxAirSpeed;

    public AudioSource jumpSound;
    public AudioSource dashSound;


    void Start()
    {
        controller.m_Grounded = false;
        staticMaxSpeed = maxSpeed;
        staticMaxAirSpeed = maxAirSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if(maxSpeed > staticMaxSpeed)
        {
            maxSpeed -= .5f; 
        }
        else
        {
            maxSpeed = staticMaxSpeed;
        }
        if(maxAirSpeed > staticMaxAirSpeed)
        {
            maxAirSpeed -= .5f;
        }
        else
        {
            maxAirSpeed = staticMaxAirSpeed;
        }


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

        if(inputAxis == -1.0f && controller.m_Grounded && horizontalMove < maxSpeed * -1.0f ) //max speed check while grounded
        {

            horizontalMove = maxSpeed * -1.0f;
            
        }
        if (inputAxis == 1.0f && controller.m_Grounded && horizontalMove > maxSpeed)
        {
            
            horizontalMove = maxSpeed;
            
        }

        if(inputAxis == -1.0f && !controller.m_Grounded && horizontalMove < maxAirSpeed * -1.0f) //max speed check in air
        {
            horizontalMove = maxAirSpeed * -1.0f;
        }

        if (inputAxis == 1.0f && !controller.m_Grounded && horizontalMove > maxAirSpeed)
        {
            horizontalMove = maxAirSpeed;
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
            doubleJump = false;
            jump = true;
            jumpTimeCounter = jumpTime;
            //rb.AddForce(new Vector2(0f, initialJumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, initialJumpForce);
            playJumpPS();
            jumpSound.Play();
        }
        
        
        
        if (jump && Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0)
            {
                rb.AddForce(new Vector2(0f, additionalJumpForce));
                jumpTimeCounter -= Time.fixedDeltaTime;

                jumpSound.Play();
            }
            else if (jumpTimeCounter < 0)
            {
                jump = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
            doubleJump = true;
        }

        //dash right
        if (Input.GetKeyDown(KeyCode.D) && dashCooldown)
        {
            if ((Time.time - lastTapTimeRight) < doubleTapThreshold)
            {
                dashCooldown = false;
                rb.AddForce(new Vector2(dashForce, 0f));
                maxSpeed = maxSpeed * 1.5f;
                dustRightPS.Play();
                dashSound.Play();
                Invoke("setDashCooldown", dashCooldownTime);
            }
            lastTapTimeRight = Time.time;

        }
        //dash left
        if (Input.GetKeyDown(KeyCode.A) && dashCooldown)
        {
            if ((Time.time - lastTapTimeLeft) < doubleTapThreshold)
            {
                dashCooldown = false;
                rb.AddForce(new Vector2(dashForce * -1.0f, 0f));
                maxAirSpeed = maxAirSpeed * 1.5f;
                dustLeftPS.Play();
                dashSound.Play();
                Invoke("setDashCooldown", dashCooldownTime);
            }
            lastTapTimeLeft = Time.time;
        }

        //double jump
        if (Input.GetKeyDown(KeyCode.Space) && doubleJump == true && jumpCooldown && doubleJumpForce > 0 )
            if ((Time.time - lastTapTimeJump) < doubleJumpThreshold)
            {
                jumpCooldown = false;
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                playJumpPS();
                Invoke("setDoubleJumpCooldown", doubleJumpCooldownTime);
        }
        lastTapTimeJump = Time.time;

    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }


    void setDashCooldown()
    {
        dashCooldown = true;
    }

    void setDoubleJumpCooldown()
    {
        jumpCooldown = true;
    }

    void playJumpPS()
    {
        jumpPS.Play();
    }

}

