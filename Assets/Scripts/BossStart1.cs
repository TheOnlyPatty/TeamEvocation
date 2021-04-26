using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart1 : MonoBehaviour
{

    public TurretBossMove tbm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col){
      if(col.isTrigger == false){
        if(col.CompareTag("Player")){
          tbm.setStarted();
        }
      }
    }
}
