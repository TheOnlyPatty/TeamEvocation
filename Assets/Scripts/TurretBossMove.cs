using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBossMove : MonoBehaviour
{

    private bool moveRight;
    private bool moveUp;
    private Rigidbody2D rb;
    private Transform ts;
    private TurretTracking turret;
    public float moveBy;
    private Vector2 pointAtPlayer;
    private bool started;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      ts = GetComponent<Transform>();
      turret = GetComponent<TurretTracking>();
      rb.gravityScale = 0f;
      moveRight = false;
      moveUp = false;
      started = false;
    }

    // Update is called once per frame
    void Update()
    {
      if(started){
        if(turret.currentHealth > (turret.maxHealth / 2)){
          if(!moveRight && !moveUp){
            // Go Left
            rb.velocity = new Vector2(-moveBy, 0f);
          }

          if(!moveRight && moveUp){
            // Go Up
            rb.velocity = new Vector2(0f, moveBy);
          }

          if(moveRight && moveUp){
            // Go Right
            rb.velocity = new Vector2(moveBy, 0f);
          }

          if(moveRight && !moveUp){
            // Go Down
            rb.velocity = new Vector2(0f, -moveBy);
          }
        }else{
          pointAtPlayer.Normalize();
          pointAtPlayer *= moveBy;
          rb.velocity = pointAtPlayer;
        }
      }
    }

    void OnCollisionEnter2D(Collision2D col){
      if(col.gameObject.tag != "Player"){
        if(!moveRight && !moveUp){
          moveUp = !moveUp;
          ts.position = new Vector2(ts.position.x + 0.4f, ts.position.y);
        }
        else if(!moveRight && moveUp){
          moveRight = !moveRight;
          ts.position = new Vector2(ts.position.x, ts.position.y - 0.4f);
        }
        else if(moveRight && moveUp){
          moveUp = !moveUp;
          ts.position = new Vector2(ts.position.x - 0.4f, ts.position.y);
        }
        else if(moveRight && !moveUp){
          moveRight = !moveRight;
          ts.position = new Vector2(ts.position.x, ts.position.y + 0.4f);
        }
        pointAtPlayer = turret.target.transform.position - transform.position;
      }
    }

    public void setStarted(){
      started = true;
    }
}
