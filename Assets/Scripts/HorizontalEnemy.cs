using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
      rb.gravityScale = 0f;
      basePos = GetComponent<Transform>().position.x;
      moveBy = 3f;
      moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
      rb.velocity = new Vector2(moveBy, rb.velocity.y);

      if(GetComponent<Transform>().position.x > (basePos + 5) && moveRight){
        moveBy *= -1f;
        moveRight = false;
      }

      if(GetComponent<Transform>().position.x < (basePos - 5) && !moveRight){
        moveBy *= -1f;
        moveRight = true;
      }
    }

    void OnCollisionEnter2D(Collision2D col){
      if(col.gameObject.tag == "Player"){
        if(col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 1){
          Destroy(gameObject);
        }else{
          // Eventually replace this with damaging the Player
          SceneManager.LoadScene("Menu");
          Destroy(col.gameObject);
        }
      }
      moveBy *= -1f;
      moveRight = !moveRight;
    }
}
