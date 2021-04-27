using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogBoss : MonoBehaviour
{

    public Transform target;
    private bool playerRight;
    private bool moveRight;
    public float moveBy;
    private Rigidbody2D rb;
    private float currentHealth;
    private float maxHealth;
    private bool jumping;
    public GameObject bullet;
    public float bulletSpeed;
    private float timer;
    private float timer2;
    private bool iframes;
    private bool started;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      moveRight = false;
      jumping = false;
      iframes = false;
      maxHealth = 3f;
      currentHealth = maxHealth;
      started = false;
    }

    // Update is called once per frame
    void Update()
    {
      if(started){
        if(!jumping){
          RunAround();
        }

        if(currentHealth < 3){
          timer += Time.deltaTime;
          if(!jumping && timer > 5f){
            Jump();
            timer = 0f;
          }
        }

        if(currentHealth < 2){
          timer2 += Time.deltaTime;
          if(timer2 > 2.5f){
            OmniAttack();
            timer2 = 0f;
          }
        }

        if(currentHealth == 0){
          SceneManager.LoadScene("Menu");
        }
      }
    }

    void Jump(){
      jumping = true;
      StartCoroutine(Attack(0.33f));
      StartCoroutine(Attack(0.66f));
      StartCoroutine(Attack(1f));
      StartCoroutine(Attack(1.33f));
      StartCoroutine(Attack(1.66f));
      if(target.transform.position.x < transform.position.x){
        playerRight = false;
      }else if(target.transform.position.x > transform.position.x){
        playerRight = true;
      }
      if(playerRight){
        // Jump Right
        rb.AddForce(new Vector2(500f, 500f));
      }else if(!playerRight){
        // Jump Left
        rb.AddForce(new Vector2(-500f, 500f));
      }
    }

    void RunAround(){
      if(target.transform.position.x < transform.position.x && playerRight){
        playerRight = false;
        StartCoroutine(switchDir());
      }else if(target.transform.position.x > transform.position.x && !playerRight){
        playerRight = true;
        StartCoroutine(switchDir());
      }
      if(moveRight){
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
      }else if(!moveRight){
        rb.velocity = new Vector2(-moveBy, rb.velocity.y);
      }
    }

    IEnumerator Attack(float holdTime){
      yield return new WaitForSecondsRealtime(holdTime);
      Vector2 direction = target.transform.position - transform.position;
      direction.Normalize();
      GameObject bulletClone;
      bulletClone = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y) + direction * 3, transform.rotation); // as GameObject;
      bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void OmniAttack(){
      Vector2 direction = target.transform.position - transform.position;
      direction.Normalize();
      // Right
      GameObject bulletRight;
      bulletRight = Instantiate(bullet, new Vector2(transform.position.x + 3f, transform.position.y), transform.rotation);
      bulletRight.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0f);
      // Left
      GameObject bulletLeft;
      bulletLeft = Instantiate(bullet, new Vector2(transform.position.x - 3f, transform.position.y), transform.rotation);
      bulletLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0f);
      // Up
      GameObject bulletUp;
      bulletUp = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 2f), transform.rotation);
      bulletUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, bulletSpeed);
      // Down
      GameObject bulletDown;
      bulletDown = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 2f), transform.rotation);
      bulletDown.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -bulletSpeed);
      // Upper Right
      GameObject bulletUR;
      bulletUR = Instantiate(bullet, new Vector2(transform.position.x + 2f, transform.position.y + 2f), transform.rotation);
      bulletUR.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, bulletSpeed);
      // Upper Left
      GameObject bulletUL;
      bulletUL = Instantiate(bullet, new Vector2(transform.position.x - 2f, transform.position.y + 2f), transform.rotation);
      bulletUL.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, bulletSpeed);
      // Lower Right
      GameObject bulletLR;
      bulletLR = Instantiate(bullet, new Vector2(transform.position.x + 2f, transform.position.y - 2f), transform.rotation);
      bulletLR.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, -bulletSpeed);
      // Lower Left
      GameObject bulletLL;
      bulletLL = Instantiate(bullet, new Vector2(transform.position.x - 2f, transform.position.y - 2f), transform.rotation);
      bulletLL.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, -bulletSpeed);
    }

    void OnCollisionEnter2D(Collision2D col){
      if(col.gameObject.tag == "Player"){
        if(!iframes && col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 8){
          currentHealth--;
          iframes = true;
          if(playerRight)
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(70000f, 30000f));
          else if(!playerRight)
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-70000f, 30000f));
          StartCoroutine(IFrames());
        }else{
          col.gameObject.GetComponent<Health>().takeDamage();
        }
      }else{
        jumping = false;
      }
    }

    IEnumerator switchDir(){
      yield return new WaitForSecondsRealtime(0.3f);
      moveRight = !moveRight;
      transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    IEnumerator IFrames(){
      GetComponent<SpriteRenderer>().color = Color.red;
      yield return new WaitForSecondsRealtime(1f);
      GetComponent<SpriteRenderer>().color = Color.white;
      iframes = false;
    }

    public void setStarted(){
      started = true;
      GetComponent<Animator>().enabled = true;
    }
}
