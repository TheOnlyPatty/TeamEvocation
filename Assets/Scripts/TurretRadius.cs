using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRadius : MonoBehaviour
{

    public TurretTracking turretAI;

    void Start(){
      turretAI = gameObject.GetComponentInParent<TurretTracking>();
    }

    void OnTriggerStay2D(Collider2D col){
      if(col.CompareTag("Player")){
        turretAI.Attack();
      }
    }
}
