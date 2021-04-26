
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurretBullet : MonoBehaviour
{
    

    void OnTriggerEnter2D(Collider2D col){
      if(col.isTrigger == false){
        if(col.CompareTag("Player")){
                col.gameObject.GetComponent<Health>().takeDamage();
                
         }
        Destroy(gameObject);
      }
    }
}

