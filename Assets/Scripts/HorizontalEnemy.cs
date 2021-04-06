using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemy : MonoBehaviour
{

    private Rigidbody2D rb;
    private float basePos;
    private float moveBy;
    private bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      //rb.gravityScale = 0f;
      basePos = GetComponent<Transform>().position.x;
      moveBy = 1f;
      moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
      rb.velocity = new Vector2(moveBy, rb.velocity.y);

      if(GetComponent<Transform>().position.x > (basePos + 2) && moveRight){
        moveBy *= -1f;
        moveRight = false;
      }

      if(GetComponent<Transform>().position.x < (basePos - 2) && !moveRight){
        moveBy *= -1f;
        moveRight = true;
      }
    }
}
