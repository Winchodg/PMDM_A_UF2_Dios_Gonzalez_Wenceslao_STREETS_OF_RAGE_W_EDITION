using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBarrelController : MonoBehaviour
{
    GameObject player;

    Collider2D col;

    Renderer barrelRend;

    float height;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");

        col = player.GetComponent<Collider2D>();

        height = col.bounds.extents.y;

        barrelRend = GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        BarrelPosition();
    }

    void BarrelPosition ()
    {    
            if ((player.transform.position.y - height) > transform.position.y)
            {
                barrelRend.sortingOrder = 6;
            }
            else
            {
                barrelRend.sortingOrder = 4;
            }
    }
}
