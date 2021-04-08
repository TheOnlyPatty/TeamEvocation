using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
      if(col.isTrigger == false){
        if(col.CompareTag("Player")){
          Destroy(col.gameObject);
        }
        Destroy(gameObject);
      }
    }
}
