using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemy : MonoBehaviour
{

    private Rigidbody2D rb;
    private float basePos;
    private float moveBy;
    private bool moveUp;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      rb.gravityScale = 0f;
      basePos = GetComponent<Transform>().position.y;
      moveBy = 1f;
      moveUp = true;
    }

    // Update is called once per frame
    void Update()
    {
      rb.velocity = new Vector2(rb.velocity.x, moveBy);

      if(GetComponent<Transform>().position.y > (basePos + 2) && moveUp){
        moveBy *= -1f;
        moveUp = false;
      }

      if(GetComponent<Transform>().position.y < (basePos - 2) && !moveUp){
        moveBy *= -1f;
        moveUp = true;
      }
    }
}
