using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTracking : MonoBehaviour
{


    public int currentHealth;
    public int maxHealth;
    public float shootInterval = 1f;
    public float bulletSpeed = 100f;
    private float bulletTimer;
    public GameObject bullet;
    public Transform target;
    public Transform shootPoint;
    public AudioSource bulletSound;
    private bool iframes;
    private bool playerRight;

    // Start is called before the first frame update
    void Start()
    {
      currentHealth = maxHealth;
      iframes = false;
    }

    // Update is called once per frame
    void Update()
    {
      RotateGameObject(target.position, 1f, 90f);

      if(currentHealth == 0){
        Destroy(gameObject);
      }

      if(Vector3.Distance(target.transform.position, transform.position) < 18){
        Attack();
      }

      if(target.transform.position.x < transform.position.x && playerRight){
        playerRight = false;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
      }else if(target.transform.position.x > transform.position.x && !playerRight){
        playerRight = true;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
      }
    }

    private void RotateGameObject(Vector3 target, float RotationSpeed, float offset)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);//RotationSpeed * Time.deltaTime);
    }

    public void Attack(){
      bulletTimer += Time.deltaTime;
      if(bulletTimer >= shootInterval){
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        GameObject bulletClone;
        bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation); // as GameObject;
        bulletSound.Play();
        bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        bulletTimer = 0;
      }
    }

    void OnCollisionEnter2D(Collision2D col){
      if(col.gameObject.tag == "Player"){
        if(col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0){
          currentHealth--;
          iframes = true;
          StartCoroutine(IFrames());
        }else{
          // Eventually replace this with damaging the Player
          Destroy(col.gameObject);
        }
      }
    }

    IEnumerator IFrames(){
      GetComponent<SpriteRenderer>().color = Color.red;
      yield return new WaitForSecondsRealtime(1f);
      GetComponent<SpriteRenderer>().color = Color.white;
      iframes = false;
    }
}
