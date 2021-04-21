using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossDoor : MonoBehaviour
{

    public Collider2D door;
    public TilemapRenderer mapRender;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col){
      if(col.isTrigger == false && col.CompareTag("Player"))
        door.enabled = true;
        mapRender.enabled = true;
    }
}
