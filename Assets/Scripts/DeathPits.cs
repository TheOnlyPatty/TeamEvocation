using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPits : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D col){
    if(col.isTrigger == false){
      if(col.CompareTag("Player")){
        col.gameObject.GetComponent<Health>().takeDamage();
        col.gameObject.GetComponent<Health>().takeDamage();
        col.gameObject.GetComponent<Health>().takeDamage();
      }
      Destroy(gameObject);
    }
  }
}
